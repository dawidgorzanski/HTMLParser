using HTMLParser.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HTMLParser.Parser.Common
{
    /// <summary>
    /// Parsing arguments passed to all additional parsers.
    /// </summary>
    internal class ParsingArguments
    {
        public int CurrentIndexInLine { get; private set; }
        public int CurrentIndex { get; private set; }
        public int CurrentLine { get; private set; }
        public Stack<HtmlElement> Stack { get; }
        public bool ArgumentsChanged { get; private set; }

        public ParsingArguments(int currentCharacterInLine, int currentCharacter, int currentLine, Stack<HtmlElement> stack)
        {
            this.CurrentIndexInLine = currentCharacterInLine;
            this.CurrentIndex = currentCharacter;
            this.CurrentLine = currentLine;
            this.Stack = stack;
        }

        public void ChangeIndexes(int currentIndex, int currentIndexInLine, int currentLine)
        {
            this.CurrentIndex = currentIndex;
            this.CurrentIndexInLine = currentIndexInLine;
            this.CurrentLine = currentLine;
            this.ArgumentsChanged = true;
        }
    }
}
