using HTMLParser.Parser.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HTMLParser.Parser.AdditionalParser
{
    /// <summary>
    /// Parser for values of script tags. Should speed up parsing of long scripts and doesn't require parsing rules for script tag in main parser.
    /// </summary>
    internal class ScriptsParser : IAdditionalParser
    {
        #region variables
        protected IList<PreParsedScriptValue> Scripts { get; set; }
        #endregion

        #region IParserImplementation
        public int ExecutionOrder => 0;
        #endregion

        #region IAdditionalParser implementation
        public void Initialize(string html)
        {
            string pattern = @"<script([\s\S]*?)</script[\n\r\s]*>";
            var matchedResults = Regex.Matches(html, pattern);
            Scripts = matchedResults.Select(res => {
                string scriptValue = res.ToString();
                var scriptValueStartIndex = scriptValue.IndexOf('>') + 1;
                scriptValue = scriptValue.Remove(0, scriptValueStartIndex);

                var scriptLength = scriptValue.LastIndexOf('<');
                scriptValue = scriptValue.Substring(0, scriptLength);

                return new PreParsedScriptValue
                {
                    Length = scriptLength,
                    StartIndex = res.Index + scriptValueStartIndex,
                    Value = scriptValue
                };
                }).ToList();
        }

        public void Parse(ParsingArguments parsingArguments)
        {
            foreach(var script in Scripts)
            {
                if (script.StartIndex <= parsingArguments.CurrentIndex && script.EndIndex >= parsingArguments.CurrentIndex)
                {
                    if (parsingArguments.Stack.TryPeek(out var topHtmlElement))
                    {
                        topHtmlElement.Value = script.Value;
                        parsingArguments.ChangeIndexes(script.EndIndex, 0, parsingArguments.CurrentLine + script.Value.Count(c => c == '\n'));
                        break;
                    }
                }
            }
        }

        #endregion

        internal class PreParsedScriptValue
        {
            public int StartIndex { get; set; }
            public int Length { get; set; }
            public int EndIndex
            {
                get
                {
                    return StartIndex + Length;
                }
            }
            public string Value { get; set; }
        }
    }
}
