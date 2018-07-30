using System;
using System.Collections.Generic;
using System.Text;

namespace HTMLParser.Model
{
    /// <summary>
    /// Body Html Element which derives from HtmlElement.
    /// </summary>
    public class BodyHtmlElement: HtmlElement
    {
        public BodyHtmlElement(): base("body")
        {

        }

        public BodyHtmlElement(HtmlElement htmlElement): this()
        {
            CopyProperties(htmlElement);
        }
    }
}
