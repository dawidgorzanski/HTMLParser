using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HTMLParser.Model;
using HTMLParser.Parser.Common;

namespace HTMLParser.Parser.PostParsers
{
    /// <summary>
    /// Clears all empty tag values (value cleared when contains only spaces, tabs and new lines).
    /// </summary>
    internal class ValuesPostParser : IPostParser
    {
        public int ExecutionOrder => -1;

        public ParsingResult PostParse(ParsingResult parsingResult)
        {
            var root = parsingResult.Root;
            TrimWhiteSpaces(root);
            return parsingResult;
        }

        private void TrimWhiteSpaces(params HtmlElement[] htmlElements)
        {
            foreach(var element in htmlElements)
            {
                if (element.Value != null)
                {
                    element.Value = element.Value.Trim();
                    string newValue = element.Value.Replace("\r\n", "");
                    newValue = element.Value.Replace("\n", "");
                    newValue = newValue.Replace("\t", "");
                    newValue = newValue.Replace(" ", "");
                    if (string.IsNullOrEmpty(newValue))
                        element.Value = "";
                }

                if(element.Elements.Any())
                    TrimWhiteSpaces(element.Elements.ToArray());
            }
        }
    }
}
