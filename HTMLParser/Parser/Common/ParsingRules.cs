using System;
using System.Collections.Generic;
using System.Text;

namespace HTMLParser.Parser.Common
{
    /// <summary>
    /// All parsing rules.
    /// </summary>
    internal static class ParsingRules
    {
        /// <summary>
        /// Dictionary for parsing states machine.
        /// </summary>
        internal static readonly Dictionary<Transition, ParsingStates> StatesTable = new Dictionary<Transition, ParsingStates>
        {
            //Value
            { new Transition(ParsingStates.Value, CharacterTypes.StartingTag), ParsingStates.PossibleStartCloseTagOrComment },
            { new Transition(ParsingStates.Value, CharacterTypes.EndingTag), ParsingStates.Value },
            { new Transition(ParsingStates.Value, CharacterTypes.CharacterDigitSign), ParsingStates.Value },
            { new Transition(ParsingStates.Value, CharacterTypes.Exclamation), ParsingStates.Value },
            { new Transition(ParsingStates.Value, CharacterTypes.Slash), ParsingStates.Value },
            { new Transition(ParsingStates.Value, CharacterTypes.Equals), ParsingStates.Value },
            { new Transition(ParsingStates.Value, CharacterTypes.SingleQuotation), ParsingStates.Value },
            { new Transition(ParsingStates.Value, CharacterTypes.DoubleQuotation), ParsingStates.Value },
            { new Transition(ParsingStates.Value, CharacterTypes.Pause), ParsingStates.Value },
            { new Transition(ParsingStates.Value, CharacterTypes.WhiteSpace), ParsingStates.Value },
            { new Transition(ParsingStates.Value, CharacterTypes.NewLine), ParsingStates.Value },
            { new Transition(ParsingStates.Value, CharacterTypes.CarretReturn), ParsingStates.Value },

            //PossibleStartCloseTagOrComment
            { new Transition(ParsingStates.PossibleStartCloseTagOrComment, CharacterTypes.StartingTag), ParsingStates.Error },
            { new Transition(ParsingStates.PossibleStartCloseTagOrComment, CharacterTypes.EndingTag), ParsingStates.Value },
            { new Transition(ParsingStates.PossibleStartCloseTagOrComment, CharacterTypes.CharacterDigitSign), ParsingStates.StartTag },
            { new Transition(ParsingStates.PossibleStartCloseTagOrComment, CharacterTypes.Exclamation), ParsingStates.PossibleStartComment1 },
            { new Transition(ParsingStates.PossibleStartCloseTagOrComment, CharacterTypes.Slash), ParsingStates.ClosingTag },
            { new Transition(ParsingStates.PossibleStartCloseTagOrComment, CharacterTypes.Equals), ParsingStates.Value },
            { new Transition(ParsingStates.PossibleStartCloseTagOrComment, CharacterTypes.SingleQuotation), ParsingStates.Value },
            { new Transition(ParsingStates.PossibleStartCloseTagOrComment, CharacterTypes.DoubleQuotation), ParsingStates.Value },
            { new Transition(ParsingStates.PossibleStartCloseTagOrComment, CharacterTypes.Pause), ParsingStates.Value },
            { new Transition(ParsingStates.PossibleStartCloseTagOrComment, CharacterTypes.WhiteSpace), ParsingStates.Value },
            { new Transition(ParsingStates.PossibleStartCloseTagOrComment, CharacterTypes.NewLine), ParsingStates.Value },
            { new Transition(ParsingStates.PossibleStartCloseTagOrComment, CharacterTypes.CarretReturn), ParsingStates.PossibleStartCloseTagOrComment },

            //StartTag
            { new Transition(ParsingStates.StartTag, CharacterTypes.StartingTag), ParsingStates.Error },
            { new Transition(ParsingStates.StartTag, CharacterTypes.EndingTag), ParsingStates.Value }, //+STACK
            { new Transition(ParsingStates.StartTag, CharacterTypes.CharacterDigitSign), ParsingStates.StartTag },
            { new Transition(ParsingStates.StartTag, CharacterTypes.Exclamation), ParsingStates.Error },
            { new Transition(ParsingStates.StartTag, CharacterTypes.Slash), ParsingStates.PossibleSelfClosingTag },
            { new Transition(ParsingStates.StartTag, CharacterTypes.Equals), ParsingStates.Error },
            { new Transition(ParsingStates.StartTag, CharacterTypes.SingleQuotation), ParsingStates.Error },
            { new Transition(ParsingStates.StartTag, CharacterTypes.DoubleQuotation), ParsingStates.Error },
            { new Transition(ParsingStates.StartTag, CharacterTypes.Pause), ParsingStates.Error },
            { new Transition(ParsingStates.StartTag, CharacterTypes.WhiteSpace), ParsingStates.PossibleAttributeName },
            { new Transition(ParsingStates.StartTag, CharacterTypes.NewLine), ParsingStates.PossibleAttributeName },
            { new Transition(ParsingStates.StartTag, CharacterTypes.CarretReturn), ParsingStates.StartTag },

            //PossibleSelfClosingTag
            { new Transition(ParsingStates.PossibleSelfClosingTag, CharacterTypes.StartingTag), ParsingStates.Error },
            { new Transition(ParsingStates.PossibleSelfClosingTag, CharacterTypes.EndingTag), ParsingStates.Value },
            { new Transition(ParsingStates.PossibleSelfClosingTag, CharacterTypes.CharacterDigitSign), ParsingStates.Error },
            { new Transition(ParsingStates.PossibleSelfClosingTag, CharacterTypes.Exclamation), ParsingStates.Error },
            { new Transition(ParsingStates.PossibleSelfClosingTag, CharacterTypes.Slash), ParsingStates.Error },
            { new Transition(ParsingStates.PossibleSelfClosingTag, CharacterTypes.Equals), ParsingStates.Error },
            { new Transition(ParsingStates.PossibleSelfClosingTag, CharacterTypes.SingleQuotation), ParsingStates.Error },
            { new Transition(ParsingStates.PossibleSelfClosingTag, CharacterTypes.DoubleQuotation), ParsingStates.Error },
            { new Transition(ParsingStates.PossibleSelfClosingTag, CharacterTypes.Pause), ParsingStates.Error },
            { new Transition(ParsingStates.PossibleSelfClosingTag, CharacterTypes.WhiteSpace), ParsingStates.Error },
            { new Transition(ParsingStates.PossibleSelfClosingTag, CharacterTypes.NewLine), ParsingStates.Error },
            { new Transition(ParsingStates.PossibleSelfClosingTag, CharacterTypes.CarretReturn), ParsingStates.PossibleSelfClosingTag },

            //ClosingTag
            { new Transition(ParsingStates.ClosingTag, CharacterTypes.StartingTag), ParsingStates.Error },
            { new Transition(ParsingStates.ClosingTag, CharacterTypes.EndingTag), ParsingStates.Value }, //-STACK
            { new Transition(ParsingStates.ClosingTag, CharacterTypes.CharacterDigitSign), ParsingStates.ClosingTag },
            { new Transition(ParsingStates.ClosingTag, CharacterTypes.Exclamation), ParsingStates.Error },
            { new Transition(ParsingStates.ClosingTag, CharacterTypes.Slash), ParsingStates.Error },
            { new Transition(ParsingStates.ClosingTag, CharacterTypes.Equals), ParsingStates.Error },
            { new Transition(ParsingStates.ClosingTag, CharacterTypes.SingleQuotation), ParsingStates.Error },
            { new Transition(ParsingStates.ClosingTag, CharacterTypes.DoubleQuotation), ParsingStates.Error },
            { new Transition(ParsingStates.ClosingTag, CharacterTypes.Pause), ParsingStates.Error },
            { new Transition(ParsingStates.ClosingTag, CharacterTypes.WhiteSpace), ParsingStates.ClosingTag },
            { new Transition(ParsingStates.ClosingTag, CharacterTypes.NewLine), ParsingStates.ClosingTag },
            { new Transition(ParsingStates.ClosingTag, CharacterTypes.CarretReturn), ParsingStates.ClosingTag },

            //PossibleAttributeName
            { new Transition(ParsingStates.PossibleAttributeName, CharacterTypes.StartingTag), ParsingStates.Error },
            { new Transition(ParsingStates.PossibleAttributeName, CharacterTypes.EndingTag), ParsingStates.Value }, //+STACK
            { new Transition(ParsingStates.PossibleAttributeName, CharacterTypes.CharacterDigitSign), ParsingStates.AttributeName },
            { new Transition(ParsingStates.PossibleAttributeName, CharacterTypes.Exclamation), ParsingStates.Error },
            { new Transition(ParsingStates.PossibleAttributeName, CharacterTypes.Slash), ParsingStates.PossibleSelfClosingTag },
            { new Transition(ParsingStates.PossibleAttributeName, CharacterTypes.Equals), ParsingStates.Error },
            { new Transition(ParsingStates.PossibleAttributeName, CharacterTypes.SingleQuotation), ParsingStates.Error },
            { new Transition(ParsingStates.PossibleAttributeName, CharacterTypes.DoubleQuotation), ParsingStates.Error },
            { new Transition(ParsingStates.PossibleAttributeName, CharacterTypes.Pause), ParsingStates.Error },
            { new Transition(ParsingStates.PossibleAttributeName, CharacterTypes.WhiteSpace), ParsingStates.PossibleAttributeName },
            { new Transition(ParsingStates.PossibleAttributeName, CharacterTypes.NewLine), ParsingStates.PossibleAttributeName },
            { new Transition(ParsingStates.PossibleAttributeName, CharacterTypes.CarretReturn), ParsingStates.PossibleAttributeName },

            //AttributeName
            { new Transition(ParsingStates.AttributeName, CharacterTypes.StartingTag), ParsingStates.Error },
            { new Transition(ParsingStates.AttributeName, CharacterTypes.EndingTag), ParsingStates.Error },
            { new Transition(ParsingStates.AttributeName, CharacterTypes.CharacterDigitSign), ParsingStates.AttributeName },
            { new Transition(ParsingStates.AttributeName, CharacterTypes.Exclamation), ParsingStates.Error },
            { new Transition(ParsingStates.AttributeName, CharacterTypes.Slash), ParsingStates.Error },
            { new Transition(ParsingStates.AttributeName, CharacterTypes.Equals), ParsingStates.PossibleAttributeValue },
            { new Transition(ParsingStates.AttributeName, CharacterTypes.SingleQuotation), ParsingStates.Error },
            { new Transition(ParsingStates.AttributeName, CharacterTypes.DoubleQuotation), ParsingStates.Error },
            { new Transition(ParsingStates.AttributeName, CharacterTypes.Pause), ParsingStates.AttributeName },
            { new Transition(ParsingStates.AttributeName, CharacterTypes.WhiteSpace), ParsingStates.PossibleEquals },
            { new Transition(ParsingStates.AttributeName, CharacterTypes.NewLine), ParsingStates.PossibleEquals },
            { new Transition(ParsingStates.AttributeName, CharacterTypes.CarretReturn), ParsingStates.AttributeName },

            //PossibleEquals
            { new Transition(ParsingStates.PossibleEquals, CharacterTypes.StartingTag), ParsingStates.Error },
            { new Transition(ParsingStates.PossibleEquals, CharacterTypes.EndingTag), ParsingStates.Error },
            { new Transition(ParsingStates.PossibleEquals, CharacterTypes.CharacterDigitSign), ParsingStates.AttributeName },
            { new Transition(ParsingStates.PossibleEquals, CharacterTypes.Exclamation), ParsingStates.Error },
            { new Transition(ParsingStates.PossibleEquals, CharacterTypes.Slash), ParsingStates.Error },
            { new Transition(ParsingStates.PossibleEquals, CharacterTypes.Equals), ParsingStates.PossibleAttributeValue },
            { new Transition(ParsingStates.PossibleEquals, CharacterTypes.SingleQuotation), ParsingStates.Error },
            { new Transition(ParsingStates.PossibleEquals, CharacterTypes.DoubleQuotation), ParsingStates.Error },
            { new Transition(ParsingStates.PossibleEquals, CharacterTypes.Pause), ParsingStates.AttributeName },
            { new Transition(ParsingStates.PossibleEquals, CharacterTypes.WhiteSpace), ParsingStates.PossibleEquals },
            { new Transition(ParsingStates.PossibleEquals, CharacterTypes.NewLine), ParsingStates.PossibleEquals },
            { new Transition(ParsingStates.PossibleEquals, CharacterTypes.CarretReturn), ParsingStates.PossibleEquals },

            //PossibleAttributeValue
            { new Transition(ParsingStates.PossibleAttributeValue, CharacterTypes.StartingTag), ParsingStates.Error },
            { new Transition(ParsingStates.PossibleAttributeValue, CharacterTypes.EndingTag), ParsingStates.Error },
            { new Transition(ParsingStates.PossibleAttributeValue, CharacterTypes.CharacterDigitSign), ParsingStates.Error },
            { new Transition(ParsingStates.PossibleAttributeValue, CharacterTypes.Exclamation), ParsingStates.Error },
            { new Transition(ParsingStates.PossibleAttributeValue, CharacterTypes.Slash), ParsingStates.Error },
            { new Transition(ParsingStates.PossibleAttributeValue, CharacterTypes.Equals), ParsingStates.Error },
            { new Transition(ParsingStates.PossibleAttributeValue, CharacterTypes.SingleQuotation), ParsingStates.AttributeValueSingleQuote },
            { new Transition(ParsingStates.PossibleAttributeValue, CharacterTypes.DoubleQuotation), ParsingStates.AttributeValueDoubleQuote },
            { new Transition(ParsingStates.PossibleAttributeValue, CharacterTypes.Pause), ParsingStates.Error },
            { new Transition(ParsingStates.PossibleAttributeValue, CharacterTypes.WhiteSpace), ParsingStates.PossibleAttributeValue },
            { new Transition(ParsingStates.PossibleAttributeValue, CharacterTypes.NewLine), ParsingStates.PossibleAttributeValue },
            { new Transition(ParsingStates.PossibleAttributeValue, CharacterTypes.CarretReturn), ParsingStates.PossibleAttributeValue },

            //AttributeValueSingleQuote
            { new Transition(ParsingStates.AttributeValueSingleQuote, CharacterTypes.StartingTag), ParsingStates.AttributeValueSingleQuote },
            { new Transition(ParsingStates.AttributeValueSingleQuote, CharacterTypes.EndingTag), ParsingStates.AttributeValueSingleQuote },
            { new Transition(ParsingStates.AttributeValueSingleQuote, CharacterTypes.CharacterDigitSign), ParsingStates.AttributeValueSingleQuote },
            { new Transition(ParsingStates.AttributeValueSingleQuote, CharacterTypes.Exclamation), ParsingStates.AttributeValueSingleQuote },
            { new Transition(ParsingStates.AttributeValueSingleQuote, CharacterTypes.Slash), ParsingStates.AttributeValueSingleQuote },
            { new Transition(ParsingStates.AttributeValueSingleQuote, CharacterTypes.Equals), ParsingStates.AttributeValueSingleQuote },
            { new Transition(ParsingStates.AttributeValueSingleQuote, CharacterTypes.SingleQuotation), ParsingStates.PossibleAttributeName },
            { new Transition(ParsingStates.AttributeValueSingleQuote, CharacterTypes.DoubleQuotation), ParsingStates.Error },
            { new Transition(ParsingStates.AttributeValueSingleQuote, CharacterTypes.Pause), ParsingStates.AttributeValueSingleQuote },
            { new Transition(ParsingStates.AttributeValueSingleQuote, CharacterTypes.WhiteSpace), ParsingStates.AttributeValueSingleQuote },
            { new Transition(ParsingStates.AttributeValueSingleQuote, CharacterTypes.NewLine), ParsingStates.AttributeValueSingleQuote },
            { new Transition(ParsingStates.AttributeValueSingleQuote, CharacterTypes.CarretReturn), ParsingStates.AttributeValueSingleQuote },

            //AttributeValueDoubleQuote
            { new Transition(ParsingStates.AttributeValueDoubleQuote, CharacterTypes.StartingTag), ParsingStates.AttributeValueDoubleQuote },
            { new Transition(ParsingStates.AttributeValueDoubleQuote, CharacterTypes.EndingTag), ParsingStates.AttributeValueDoubleQuote },
            { new Transition(ParsingStates.AttributeValueDoubleQuote, CharacterTypes.CharacterDigitSign), ParsingStates.AttributeValueDoubleQuote },
            { new Transition(ParsingStates.AttributeValueDoubleQuote, CharacterTypes.Exclamation), ParsingStates.AttributeValueDoubleQuote },
            { new Transition(ParsingStates.AttributeValueDoubleQuote, CharacterTypes.Slash), ParsingStates.AttributeValueDoubleQuote },
            { new Transition(ParsingStates.AttributeValueDoubleQuote, CharacterTypes.Equals), ParsingStates.AttributeValueDoubleQuote },
            { new Transition(ParsingStates.AttributeValueDoubleQuote, CharacterTypes.SingleQuotation), ParsingStates.AttributeValueDoubleQuote },
            { new Transition(ParsingStates.AttributeValueDoubleQuote, CharacterTypes.DoubleQuotation), ParsingStates.PossibleAttributeName },
            { new Transition(ParsingStates.AttributeValueDoubleQuote, CharacterTypes.Pause), ParsingStates.AttributeValueDoubleQuote },
            { new Transition(ParsingStates.AttributeValueDoubleQuote, CharacterTypes.WhiteSpace), ParsingStates.AttributeValueDoubleQuote },
            { new Transition(ParsingStates.AttributeValueDoubleQuote, CharacterTypes.NewLine), ParsingStates.AttributeValueDoubleQuote },
            { new Transition(ParsingStates.AttributeValueDoubleQuote, CharacterTypes.CarretReturn), ParsingStates.AttributeValueDoubleQuote },

            //PossibleStartComment1
            { new Transition(ParsingStates.PossibleStartComment1, CharacterTypes.StartingTag), ParsingStates.Value },
            { new Transition(ParsingStates.PossibleStartComment1, CharacterTypes.EndingTag), ParsingStates.Value },
            { new Transition(ParsingStates.PossibleStartComment1, CharacterTypes.CharacterDigitSign), ParsingStates.Value },
            { new Transition(ParsingStates.PossibleStartComment1, CharacterTypes.Exclamation), ParsingStates.Value },
            { new Transition(ParsingStates.PossibleStartComment1, CharacterTypes.Slash), ParsingStates.Value },
            { new Transition(ParsingStates.PossibleStartComment1, CharacterTypes.Equals), ParsingStates.Value },
            { new Transition(ParsingStates.PossibleStartComment1, CharacterTypes.SingleQuotation), ParsingStates.Value },
            { new Transition(ParsingStates.PossibleStartComment1, CharacterTypes.DoubleQuotation), ParsingStates.Value },
            { new Transition(ParsingStates.PossibleStartComment1, CharacterTypes.Pause), ParsingStates.PossibleStartComment2 },
            { new Transition(ParsingStates.PossibleStartComment1, CharacterTypes.WhiteSpace), ParsingStates.Value },
            { new Transition(ParsingStates.PossibleStartComment1, CharacterTypes.NewLine), ParsingStates.Value },
            { new Transition(ParsingStates.PossibleStartComment1, CharacterTypes.CarretReturn), ParsingStates.PossibleStartComment1 },

            //PossibleStartComment2
            { new Transition(ParsingStates.PossibleStartComment2, CharacterTypes.StartingTag), ParsingStates.Value },
            { new Transition(ParsingStates.PossibleStartComment2, CharacterTypes.EndingTag), ParsingStates.Value },
            { new Transition(ParsingStates.PossibleStartComment2, CharacterTypes.CharacterDigitSign), ParsingStates.Value },
            { new Transition(ParsingStates.PossibleStartComment2, CharacterTypes.Exclamation), ParsingStates.Value },
            { new Transition(ParsingStates.PossibleStartComment2, CharacterTypes.Slash), ParsingStates.Value },
            { new Transition(ParsingStates.PossibleStartComment2, CharacterTypes.Equals), ParsingStates.Value },
            { new Transition(ParsingStates.PossibleStartComment2, CharacterTypes.SingleQuotation), ParsingStates.Value },
            { new Transition(ParsingStates.PossibleStartComment2, CharacterTypes.DoubleQuotation), ParsingStates.Value },
            { new Transition(ParsingStates.PossibleStartComment2, CharacterTypes.Pause), ParsingStates.Comment },
            { new Transition(ParsingStates.PossibleStartComment2, CharacterTypes.WhiteSpace), ParsingStates.Value },
            { new Transition(ParsingStates.PossibleStartComment2, CharacterTypes.NewLine), ParsingStates.Value },
            { new Transition(ParsingStates.PossibleStartComment2, CharacterTypes.CarretReturn), ParsingStates.PossibleStartComment2 },

            //Comment
            { new Transition(ParsingStates.Comment, CharacterTypes.StartingTag), ParsingStates.Comment },
            { new Transition(ParsingStates.Comment, CharacterTypes.EndingTag), ParsingStates.Comment },
            { new Transition(ParsingStates.Comment, CharacterTypes.CharacterDigitSign), ParsingStates.Comment },
            { new Transition(ParsingStates.Comment, CharacterTypes.Exclamation), ParsingStates.Comment },
            { new Transition(ParsingStates.Comment, CharacterTypes.Slash), ParsingStates.Comment },
            { new Transition(ParsingStates.Comment, CharacterTypes.Equals), ParsingStates.Comment },
            { new Transition(ParsingStates.Comment, CharacterTypes.SingleQuotation), ParsingStates.Comment },
            { new Transition(ParsingStates.Comment, CharacterTypes.DoubleQuotation), ParsingStates.Comment },
            { new Transition(ParsingStates.Comment, CharacterTypes.Pause), ParsingStates.PossibleEndComment1 },
            { new Transition(ParsingStates.Comment, CharacterTypes.WhiteSpace), ParsingStates.Comment },
            { new Transition(ParsingStates.Comment, CharacterTypes.NewLine), ParsingStates.Comment },
            { new Transition(ParsingStates.Comment, CharacterTypes.CarretReturn), ParsingStates.Comment },

            //PossibleEndComment1
            { new Transition(ParsingStates.PossibleEndComment1, CharacterTypes.StartingTag), ParsingStates.Comment },
            { new Transition(ParsingStates.PossibleEndComment1, CharacterTypes.EndingTag), ParsingStates.Comment },
            { new Transition(ParsingStates.PossibleEndComment1, CharacterTypes.CharacterDigitSign), ParsingStates.Comment },
            { new Transition(ParsingStates.PossibleEndComment1, CharacterTypes.Exclamation), ParsingStates.Comment },
            { new Transition(ParsingStates.PossibleEndComment1, CharacterTypes.Slash), ParsingStates.Comment },
            { new Transition(ParsingStates.PossibleEndComment1, CharacterTypes.Equals), ParsingStates.Comment },
            { new Transition(ParsingStates.PossibleEndComment1, CharacterTypes.SingleQuotation), ParsingStates.Comment },
            { new Transition(ParsingStates.PossibleEndComment1, CharacterTypes.DoubleQuotation), ParsingStates.Comment },
            { new Transition(ParsingStates.PossibleEndComment1, CharacterTypes.Pause), ParsingStates.PossibleEndComment2 },
            { new Transition(ParsingStates.PossibleEndComment1, CharacterTypes.WhiteSpace), ParsingStates.Comment },
            { new Transition(ParsingStates.PossibleEndComment1, CharacterTypes.NewLine), ParsingStates.Comment },
            { new Transition(ParsingStates.PossibleEndComment1, CharacterTypes.CarretReturn), ParsingStates.PossibleEndComment1 },

            //PossibleEndComment2
            { new Transition(ParsingStates.PossibleEndComment2, CharacterTypes.StartingTag), ParsingStates.Comment },
            { new Transition(ParsingStates.PossibleEndComment2, CharacterTypes.EndingTag), ParsingStates.CommentEnd },
            { new Transition(ParsingStates.PossibleEndComment2, CharacterTypes.CharacterDigitSign), ParsingStates.Comment },
            { new Transition(ParsingStates.PossibleEndComment2, CharacterTypes.Exclamation), ParsingStates.Comment },
            { new Transition(ParsingStates.PossibleEndComment2, CharacterTypes.Slash), ParsingStates.Comment },
            { new Transition(ParsingStates.PossibleEndComment2, CharacterTypes.Equals), ParsingStates.Comment },
            { new Transition(ParsingStates.PossibleEndComment2, CharacterTypes.SingleQuotation), ParsingStates.Comment },
            { new Transition(ParsingStates.PossibleEndComment2, CharacterTypes.DoubleQuotation), ParsingStates.Comment },
            { new Transition(ParsingStates.PossibleEndComment2, CharacterTypes.Pause), ParsingStates.Comment },
            { new Transition(ParsingStates.PossibleEndComment2, CharacterTypes.WhiteSpace), ParsingStates.Comment },
            { new Transition(ParsingStates.PossibleEndComment2, CharacterTypes.NewLine), ParsingStates.Comment },
            { new Transition(ParsingStates.PossibleEndComment2, CharacterTypes.CarretReturn), ParsingStates.PossibleEndComment2 },

            //CommentEnd
            { new Transition(ParsingStates.CommentEnd, CharacterTypes.StartingTag), ParsingStates.PossibleStartCloseTagOrComment },
            { new Transition(ParsingStates.CommentEnd, CharacterTypes.EndingTag), ParsingStates.Value },
            { new Transition(ParsingStates.CommentEnd, CharacterTypes.CharacterDigitSign), ParsingStates.Value },
            { new Transition(ParsingStates.CommentEnd, CharacterTypes.Exclamation), ParsingStates.Value },
            { new Transition(ParsingStates.CommentEnd, CharacterTypes.Slash), ParsingStates.Value },
            { new Transition(ParsingStates.CommentEnd, CharacterTypes.Equals), ParsingStates.Value },
            { new Transition(ParsingStates.CommentEnd, CharacterTypes.SingleQuotation), ParsingStates.Value },
            { new Transition(ParsingStates.CommentEnd, CharacterTypes.DoubleQuotation), ParsingStates.Value },
            { new Transition(ParsingStates.CommentEnd, CharacterTypes.Pause), ParsingStates.Value },
            { new Transition(ParsingStates.CommentEnd, CharacterTypes.WhiteSpace), ParsingStates.Value },
            { new Transition(ParsingStates.CommentEnd, CharacterTypes.NewLine), ParsingStates.Value },
            { new Transition(ParsingStates.CommentEnd, CharacterTypes.CarretReturn), ParsingStates.CommentEnd },
        };

        /// <summary>
        /// Dictionary for additional operations which should be executed while moving from one state to another.
        /// </summary>
        internal static readonly Dictionary<ParsingStateChange, AdditionalOperations[]> AdditionalParsingOperations = new Dictionary<ParsingStateChange, AdditionalOperations[]>
        {
            { new ParsingStateChange(ParsingStates.StartTag, ParsingStates.PossibleAttributeName), new[] { AdditionalOperations.PushHtmlElementToStack, AdditionalOperations.ClearBuffer } },
            { new ParsingStateChange(ParsingStates.StartTag, ParsingStates.Value), new[] { AdditionalOperations.PushHtmlElementToStack, AdditionalOperations.CheckIfAutoClosingTag, AdditionalOperations.ClearBuffer } },
            { new ParsingStateChange(ParsingStates.StartTag, ParsingStates.PossibleSelfClosingTag), new[] { AdditionalOperations.PushHtmlElementToStack, AdditionalOperations.ClearBuffer} },
            { new ParsingStateChange(ParsingStates.StartTag, ParsingStates.StartTag), new[] { AdditionalOperations.AddCharacterToBuffer } },

            { new ParsingStateChange(ParsingStates.PossibleSelfClosingTag, ParsingStates.Value), new[] { AdditionalOperations.PopSelfClosingElementFromStack } },

            { new ParsingStateChange(ParsingStates.ClosingTag, ParsingStates.Value), new[] { AdditionalOperations.PopHtmlElementFromStack, AdditionalOperations.ClearBuffer } },
            { new ParsingStateChange(ParsingStates.ClosingTag, ParsingStates.ClosingTag), new[] { AdditionalOperations.AddCharacterToBuffer } },

            { new ParsingStateChange(ParsingStates.AttributeName, ParsingStates.PossibleEquals), new[] { AdditionalOperations.CreateAttributeName, AdditionalOperations.ClearBuffer } },
            { new ParsingStateChange(ParsingStates.AttributeName, ParsingStates.PossibleAttributeValue), new[] { AdditionalOperations.CreateAttributeName, AdditionalOperations.ClearBuffer } },
            { new ParsingStateChange(ParsingStates.AttributeName, ParsingStates.AttributeName), new[] { AdditionalOperations.AddCharacterToBuffer } },

            { new ParsingStateChange(ParsingStates.AttributeValueDoubleQuote, ParsingStates.PossibleAttributeName), new[] { AdditionalOperations.CreateAttributeValue, AdditionalOperations.ClearBuffer } },
            { new ParsingStateChange(ParsingStates.AttributeValueDoubleQuote, ParsingStates.AttributeValueDoubleQuote), new[] { AdditionalOperations.AddCharacterToBuffer } },

            { new ParsingStateChange(ParsingStates.AttributeValueSingleQuote, ParsingStates.PossibleAttributeName), new[] { AdditionalOperations.CreateAttributeValue, AdditionalOperations.ClearBuffer } },
            { new ParsingStateChange(ParsingStates.AttributeValueSingleQuote, ParsingStates.AttributeValueSingleQuote), new[] { AdditionalOperations.AddCharacterToBuffer } },

            { new ParsingStateChange(ParsingStates.PossibleStartCloseTagOrComment, ParsingStates.StartTag), new[] { AdditionalOperations.ClearBuffer, AdditionalOperations.AddCharacterToBuffer } },

            { new ParsingStateChange(ParsingStates.PossibleAttributeName, ParsingStates.AttributeName), new[] { AdditionalOperations.AddCharacterToBuffer } },
            { new ParsingStateChange(ParsingStates.PossibleAttributeName, ParsingStates.Value), new[] { AdditionalOperations.CheckIfAutoClosingTag } },

            { new ParsingStateChange(ParsingStates.Value, ParsingStates.Value), new[] { AdditionalOperations.CreateElementValue } },
        };

        /// <summary>
        /// HTML tags which cannot be closed
        /// </summary>
        internal static readonly List<string> HtmlTagsForbiddenToBeClosed = new List<string>
        {
            "img", "input", "br", "hr", "meta",
        };

        /// <summary>
        /// HTML tags which can be closed when another same tag appeared.
        /// </summary>
        internal static readonly List<string> HtmlAutoClosingTags = new List<string>
        {
            "html", "head", "body", "p", "dt", "dd", "li", "option", "thead", "th", "tbody", "tr", "td", "tfoot", "colgroup", "link",
        };

        /// <summary>
        /// Selected HTML attribute names.
        /// </summary>
        internal static class HtmlAttributeNames
        {
            internal const string Value = "value";
        }

        /// <summary>
        /// Selected HTML tag names.
        /// </summary>
        internal static class SignificantHtmlTagNames
        {
            internal const string title = "title";
            internal const string html = "html";
            internal const string head = "head";
            internal const string body = "body";
        }
    }
}
