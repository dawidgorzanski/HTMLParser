using System;
using System.Collections.Generic;
using System.Text;

namespace HTMLParser.Parser.Common
{
    /// <summary>
    /// Possible character types.
    /// </summary>
    internal enum CharacterTypes
    {
        StartingTag,
        EndingTag,
        CharacterDigitSign,
        SingleQuotation,
        DoubleQuotation,
        Slash,
        Equals,
        Exclamation,
        Pause,
        WhiteSpace,
        NewLine,
        CarretReturn
    }
}
