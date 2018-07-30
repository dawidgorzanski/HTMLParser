using HTMLParser.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HTMLParser.Helpers
{
    /// <summary>
    /// Factory for creating HTML Elements.
    /// </summary>
    internal static class HtmlElementFactory
    {
        /// <summary>
        /// Creates HTMLElement based on name.
        /// </summary>
        /// <param name="name">Name of HTMLElement which should be created.</param>
        /// <returns>HtmlElement</returns>
        internal static HtmlElement CreateByName(string name)
        {
            switch(name.ToLower())
            {
                case "body":
                    {
                        return new BodyHtmlElement();
                    }
                case "head":
                    {
                        return new HeadHtmlElement();
                    }
                default:
                    {
                        return new HtmlElement(name);
                    }
            }
        }
    }
}
