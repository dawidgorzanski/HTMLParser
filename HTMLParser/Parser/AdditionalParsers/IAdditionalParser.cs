using HTMLParser.Parser.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HTMLParser.Parser.AdditionalParser
{
    /// <summary>
    /// Interface for additional parser which is executed after parsing every character of HTML document. 
    /// </summary>
    internal interface IAdditionalParser : IParser
    {
        void Initialize(string html);
        void Parse(ParsingArguments parsingArguments);
    }
}
