using HTMLParser.Parser.Common;
using HTMLParser.Parser.PostParsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTMLParser.Parser.Managers
{
    /// <summary>
    /// Manages post parsers which implements IPostParser interface.
    /// </summary>
    internal sealed class PostParsersManager
    {
        private List<IPostParser> parsers;

        public PostParsersManager(IEnumerable<IPostParser> parsers)
        {
            this.parsers = new List<IPostParser>();
            this.parsers.AddRange(parsers);
        }

        public ParsingResult PostParse(ParsingResult parsingResult)
        {
            foreach (var parser in parsers.OrderBy(par => par.ExecutionOrder))
            {
                var newResult = parser.PostParse(parsingResult);
                parsingResult.Merge(newResult);
            }

            return parsingResult;
        }
    }
}
