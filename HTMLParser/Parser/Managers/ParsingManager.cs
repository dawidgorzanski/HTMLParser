using HTMLParser.Model;
using HTMLParser.Parser.MainParsers;
using HTMLParser.Parser.PostParsers;
using HTMLParser.Parser.AdditionalParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTMLParser.Parser.Managers
{
    /// <summary>
    /// Manages additional, main and post parsers managers.
    /// </summary>
    internal sealed class ParsingManager
    {
        private MainParserManager mainParserManager;
        private AdditionalParsersManager additionalParsersManager;
        private PostParsersManager postParsersManager;

        public ParsingManager()
        {
            //Dependency injection possible
            //additional parsers
            additionalParsersManager = new AdditionalParsersManager(new[] { new ScriptsParser() });

            //mainParser
            mainParserManager = new MainParserManager(new MainParser());

            //Post parsers
            postParsersManager = new PostParsersManager(new IPostParser[] { new ValuesPostParser(), new HtmlStackToHtmlDocumentParser() });
        }

        public HtmlDocument Parse(string html)
        {
            additionalParsersManager.InitializeAdditionalParsers(html);
            var parsingResult = mainParserManager.Parse(html, additionalParsersManager);
            postParsersManager.PostParse(parsingResult);
            return parsingResult.HtmlDocument;
        }
    }
}
