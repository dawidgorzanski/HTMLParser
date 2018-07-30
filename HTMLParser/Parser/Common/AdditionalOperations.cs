using System;
using System.Collections.Generic;
using System.Text;

namespace HTMLParser.Parser.Common
{
    /// <summary>
    /// Additional operations to execute after parsing every character of HTML.
    /// </summary>
    internal enum AdditionalOperations
    {
        None,
        ClearBuffer,
        AddCharacterToBuffer,
        PushHtmlElementToStack,
        PopHtmlElementFromStack,
        PopSelfClosingElementFromStack,
        CreateAttributeName,
        CreateAttributeValue,
        CreateElementValue,
        CheckIfAutoClosingTag
    }
}
