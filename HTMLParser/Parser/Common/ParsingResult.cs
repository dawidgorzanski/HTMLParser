using HTMLParser.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HTMLParser.Parser.Common
{
    /// <summary>
    /// Result of parsing passed to all post parsers.
    /// </summary>
    internal class ParsingResult
    {
        public string Html { get; protected set; }
        public HtmlElement Root { get; set; }
        public HtmlDocument HtmlDocument { get; set; }

        public ParsingResult(string html)
        {
            this.Html = html;
        }

        public ParsingResult Merge(ParsingResult resultToMerge)
        {
            this.Html = resultToMerge.Html;
            this.Root = resultToMerge.Root;
            this.HtmlDocument = resultToMerge.HtmlDocument;

            return this;
        }
    }
}
