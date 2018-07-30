using System;
using System.Collections.Generic;
using System.Text;

namespace HTMLParser.Parser.Common
{
    /// <summary>
    /// Base interface for all parsers.
    /// </summary>
    internal interface IParser
    {
        int ExecutionOrder { get; }
    }
}
