using HTMLParser.Model;
using HTMLParser.Parser.AdditionalParser;
using HTMLParser.Parser.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTMLParser.Parser.Managers
{
    /// <summary>
    /// Manages additional parsers which imlements IAdditionalParser interface. Executes all additional parsers in executing order.
    /// </summary>
    internal sealed class AdditionalParsersManager
    {
        private List<IAdditionalParser> parsers;

        public AdditionalParsersManager(IEnumerable<IAdditionalParser> parsers)
        {
            this.parsers = new List<IAdditionalParser>();
            this.parsers.AddRange(parsers);
        }

        public void InitializeAdditionalParsers(string html)
        {
            foreach (var parser in parsers.OrderBy(par => par.ExecutionOrder))
            {
                parser.Initialize(html);
            }
        }

        public void ExecuteAdditionalParsers(ParsingArguments parsingArguments)
        {
            foreach(var parser in parsers.OrderBy(par => par.ExecutionOrder))
            {
                parser.Parse(parsingArguments);
            }
        }
    }
}
