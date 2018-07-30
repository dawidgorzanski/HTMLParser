using HTMLParser.Parser.Common;
using HTMLParser.Parser.MainParsers;
using System;
using System.Collections.Generic;
using System.Text;

namespace HTMLParser.Parser.PostParsers
{
    /// <summary>
    /// Interface for parsers executed after main parsing.
    /// </summary>
    internal interface IPostParser : IParser
    {
        ParsingResult PostParse(ParsingResult parsingResult);
    }
}
