using HTMLParser.Model;
using HTMLParser.Parser.Common;
using HTMLParser.Parser.AdditionalParser;
using System;
using System.Collections.Generic;
using System.Text;

namespace HTMLParser.Parser.MainParsers
{
    /// <summary>
    /// Interface for main parser.
    /// </summary>
    internal interface IMainParser
    {
        bool Initialized { get; }
        bool Validated { get; }
        int CurrentLine { get; }
        int CurrentIndexInLine { get; }
        int CurrentIndex { get; }
        bool ParsingFinished { get; }
        ParsingResult ParsingResult { get; }

        void Initialize(string html);
        bool ExecuteStep(ParsingArguments modifiedArguments, out ParsingArguments parsingArguments);
        void Validate();
    }
}
