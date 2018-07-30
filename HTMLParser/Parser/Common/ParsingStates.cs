using System;
using System.Collections.Generic;
using System.Text;

namespace HTMLParser.Parser.Common
{
    /// <summary>
    /// Possible parsing states.
    /// </summary>
    internal enum ParsingStates
    {
        Value,
        PossibleStartCloseTagOrComment,
        StartTag,
        PossibleSelfClosingTag,
        PossibleStartComment1,
        PossibleStartComment2,
        Comment,
        PossibleEndComment1,
        PossibleEndComment2,
        CommentEnd,
        ClosingTag,
        PossibleAttributeName,
        AttributeName,
        PossibleEquals,
        PossibleAttributeValue,
        AttributeValueSingleQuote,
        AttributeValueDoubleQuote,
        PreviousState,
        Error
    }
}
