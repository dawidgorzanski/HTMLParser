using HTMLParser.Model;
using HTMLParser.Parser.Common;
using HTMLParser.Parser.MainParsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTMLParser.Parser.PostParsers
{
    /// <summary>
    /// Creates HtmlDocument from HtmlElement.
    /// </summary>
    internal class HtmlStackToHtmlDocumentParser : IPostParser
    {
        #region IParserImplementation
        public int ExecutionOrder => 0;
        #endregion


        public ParsingResult PostParse(ParsingResult parsingResult)
        {
            HtmlDocument htmlDocument = new HtmlDocument(parsingResult.Root);
            parsingResult.HtmlDocument = htmlDocument;
            return parsingResult;
        }
    }
}
