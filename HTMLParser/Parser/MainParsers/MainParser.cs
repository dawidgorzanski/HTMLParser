using HTMLParser.Exceptions;
using HTMLParser.Helpers;
using HTMLParser.Model;
using HTMLParser.Parser.Common;
using HTMLParser.Parser.AdditionalParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTMLParser.Parser.MainParsers
{
    /// <summary>
    /// Main HTML parser.
    /// </summary>
    internal class MainParser : IMainParser
    {
        public bool Initialized { get; protected set; }
        public bool Validated { get; protected set; }
        public int CurrentLine { get; protected set; }
        public int CurrentIndexInLine { get; protected set; }
        public int CurrentIndex { get; protected set; }
        public bool ParsingFinished
        {
            get
            {
                return html.Length == CurrentIndex;
            }
        }
        public ParsingResult ParsingResult
        {
            get
            {
                if (!ParsingFinished || !Validated)
                    return null;

                ParsingResult parsingResult = new ParsingResult(html);
                parsingResult.Root = lastPopedElementFromStack;
                return parsingResult;
            }
        }

        private HtmlElement lastPopedElementFromStack { get; set; }
        private Stack<HtmlElement> stack;
        private string html;
        private TransitionManager transitionManager;
        private StringBuilder buffer;


        public void Initialize(string html)
        {
            this.html = html;
            Validated = false;
            CurrentIndex = 0;
            CurrentLine = 1;
            CurrentIndexInLine = 0;
            stack = new Stack<HtmlElement>();
            transitionManager = new TransitionManager();
            buffer = new StringBuilder();
        }

        public bool ExecuteStep(ParsingArguments modifiedArguments, out ParsingArguments parsingArguments)
        {
            parsingArguments = null;

            if (modifiedArguments != null && modifiedArguments.ArgumentsChanged)
            {
                UpdateModifiedCounters(modifiedArguments);
            }

            if (CurrentIndex >= html.Length)
                return false;

            char currentCharacter = html[CurrentIndex];

            CharacterTypes characterType = currentCharacter.ToCharacterType();

            UpdateCounters(characterType);

            transitionManager.Move(characterType, out AdditionalOperations[] additionalOperations);

            HandleState(transitionManager.CurrentState);
            HandleAdditionalOperations(additionalOperations, currentCharacter, buffer);

            parsingArguments = new ParsingArguments(CurrentIndexInLine, CurrentIndex, CurrentLine, stack);

            return true;
        }

        private void UpdateModifiedCounters(ParsingArguments modifiedArguments)
        {
            CurrentIndex = modifiedArguments.CurrentIndex;
            CurrentIndexInLine = modifiedArguments.CurrentIndexInLine;
            CurrentLine = modifiedArguments.CurrentLine;
        }

        private void UpdateCounters(CharacterTypes characterType)
        {
            CurrentIndex++;
            if (characterType == CharacterTypes.NewLine)
            {
                CurrentLine++;
                CurrentIndexInLine = 1;
            }
            else
            {
                CurrentIndexInLine++;
            }
        }

        private void HandleState(ParsingStates parsingState)
        {
            if (parsingState == ParsingStates.Error)
                throw new InvalidSyntaxException($"Invalid syntax at line: {CurrentLine}, position: {CurrentIndexInLine}");
        }

        private void HandleAdditionalOperations(AdditionalOperations[] additionalOperation, char character, StringBuilder buffer)
        {
            if (additionalOperation == null)
                return;

            foreach (AdditionalOperations value in additionalOperation)
            {
                switch (value)
                {
                    case AdditionalOperations.ClearBuffer:
                        {
                            buffer.Clear();
                            break;
                        }
                    case AdditionalOperations.AddCharacterToBuffer:
                        {
                            buffer.Append(character);
                            break;
                        }
                    case AdditionalOperations.PushHtmlElementToStack:
                        {
                            var htmlElement = HtmlElementFactory.CreateByName(buffer.ToString());

                            //Create fake parent element if only body is parsed
                            if (!stack.Any() && !htmlElement.Name.Equals(ParsingRules.SignificantHtmlTagNames.html))
                            {
                                //Create fake parent element
                                HtmlElement fakeParentElement = new HtmlElement(ParsingRules.SignificantHtmlTagNames.html);
                                stack.Push(fakeParentElement);
                            }

                            if (stack.TryPeek(out var topHtmlElement))
                            {
                                //case when <li> not closed and another <li> appeared
                                if (ParsingRules.HtmlAutoClosingTags.Contains(topHtmlElement.Name) && topHtmlElement.Name.Equals(htmlElement.Name))
                                {
                                    lastPopedElementFromStack = stack.Pop();
                                    stack.TryPeek(out topHtmlElement);
                                }

                                if (topHtmlElement != null)
                                    topHtmlElement.AddElement(htmlElement);
                            }

                            stack.Push(htmlElement);

                            break;
                        }
                    case AdditionalOperations.PopHtmlElementFromStack:
                        {
                            lastPopedElementFromStack = stack.Pop();
                            //case when <li> not closed and </ul> closing tag appears
                            while (ParsingRules.HtmlAutoClosingTags.Contains(lastPopedElementFromStack.Name) && stack.Any()
                                && !lastPopedElementFromStack.Name.Equals(buffer.ToString()))
                            {
                                lastPopedElementFromStack = stack.Pop();
                            }

                            if (!lastPopedElementFromStack.Name.Equals(buffer.ToString()))
                                throw new InvalidSyntaxException($"Invalid syntax at line: {CurrentLine}, position: {CurrentIndexInLine}");
                            break;
                        }
                    case AdditionalOperations.PopSelfClosingElementFromStack:
                        {
                            lastPopedElementFromStack = stack.Pop();
                            lastPopedElementFromStack.SelfClosed = true;
                            break;
                        }
                    case AdditionalOperations.CheckIfAutoClosingTag:
                        {
                            //Pop elements which can be closed without closing tag - meta, img
                            if (stack.TryPeek(out var topHtmlElement))
                            {
                                if (ParsingRules.HtmlTagsForbiddenToBeClosed.Contains(topHtmlElement.Name))
                                {
                                    lastPopedElementFromStack = stack.Pop();
                                    lastPopedElementFromStack.SelfClosed = true;
                                }
                            }

                            break;
                        }
                    case AdditionalOperations.CreateAttributeName:
                        {
                            stack.Peek().Attributes.Add(new HtmlAttribute(buffer.ToString()));
                            break;
                        }
                    case AdditionalOperations.CreateAttributeValue:
                        {
                            var topHtmlElement = stack.Peek();
                            var attribute = topHtmlElement.Attributes.LastOrDefault();
                            attribute.Value = buffer.ToString();

                            if (attribute.Name.ToLower().Equals(ParsingRules.HtmlAttributeNames.Value))
                                topHtmlElement.Value = attribute.Value;

                            break;
                        }
                    case AdditionalOperations.CreateElementValue:
                        {
                            if (stack.TryPeek(out var topHtmlElement))
                            {
                                topHtmlElement.Value += character;
                            }

                            break;
                        }
                }
            }
        }

        public void Validate()
        {
            if (stack.Count > 1)
                throw new InvalidSyntaxException("Not all elements have closing tag");

            if (stack.Count == 1)
            {
                lastPopedElementFromStack = stack.Pop();
                if (lastPopedElementFromStack.Name.Equals(ParsingRules.SignificantHtmlTagNames.html))
                    lastPopedElementFromStack = stack.Pop();
                else
                    throw new InvalidSyntaxException("Not all elements have closing tag");
            }

            Validated = true;
        }
    }
}
