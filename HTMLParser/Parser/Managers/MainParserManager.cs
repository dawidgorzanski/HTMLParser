using HTMLParser.Parser.Common;
using HTMLParser.Parser.MainParsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HTMLParser.Parser.Managers
{
    /// <summary>
    /// Manages main parser which implements IMainParser interface. Executes AdditionalParsersManager.
    /// </summary>
    internal sealed class MainParserManager
    {
        private IMainParser mainParser;
        public MainParserManager(IMainParser mainParser)
        {
            this.mainParser = mainParser;
        }

        public ParsingResult Parse(string html, AdditionalParsersManager additionalParsersManager)
        {
            mainParser.Initialize(html);

            ParsingArguments parsingArguments = null;
            while (mainParser.ExecuteStep(parsingArguments, out parsingArguments))
            {
                additionalParsersManager.ExecuteAdditionalParsers(parsingArguments);
            }

            mainParser.Validate();
            return mainParser.ParsingResult;
        }
    }
}
