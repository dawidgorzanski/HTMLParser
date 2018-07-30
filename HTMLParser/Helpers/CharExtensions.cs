using HTMLParser.Parser.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HTMLParser.Helpers
{
    /// <summary>
    /// Char extensions
    /// </summary>
    internal static class CharExtensions
    {
        private static Dictionary<char, CharacterTypes> characterTypesDictionary = new Dictionary<char, CharacterTypes>
        {
            { '<', CharacterTypes.StartingTag },
            { '>', CharacterTypes.EndingTag },
            { '!', CharacterTypes.Exclamation },
            { '/', CharacterTypes.Slash },
            { '=', CharacterTypes.Equals },
            { '"', CharacterTypes.DoubleQuotation },
            { '\'', CharacterTypes.SingleQuotation },
            { '-', CharacterTypes.Pause },
            { ' ', CharacterTypes.WhiteSpace },
            { '\r', CharacterTypes.CarretReturn },
            { '\n', CharacterTypes.NewLine },
        };
        /// <summary>
        /// Returns character type enum based on currect character.
        /// </summary>
        /// <param name="value">Character which should be converted to CharacterTypes enum.</param>
        /// <returns>CharacterTypes</returns>
        public static CharacterTypes ToCharacterType(this char value)
        {
            return characterTypesDictionary.ContainsKey(value) ? characterTypesDictionary[value] : CharacterTypes.CharacterDigitSign;
        }
    }
}
