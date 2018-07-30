using System;
using System.Collections.Generic;
using System.Text;

namespace HTMLParser.Helpers
{
    /// <summary>
    /// Helper for strings.
    /// </summary>
    internal static class StringHelper
    {
        /// <summary>
        /// Generate tabs (two spaces).
        /// </summary>
        /// <param name="size">Number of tabs.</param>
        /// <returns></returns>
        internal static string GenerateTabs(int size)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < size; i++)
                builder.Append("  ");

            return builder.ToString();
        }
    }
}
