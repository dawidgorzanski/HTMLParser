using HTMLParser.Parser.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTMLParser.Model
{
    /// <summary>
    /// Head Html Element which derives from HtmlElement.
    /// </summary>
    public class HeadHtmlElement: HtmlElement
    {
        public string Title { get; protected set; }

        public HeadHtmlElement(): base("head")
        {

        }

        public HeadHtmlElement(HtmlElement htmlElement): this()
        {
            CopyProperties(htmlElement);
        }

        protected override void CopyProperties(HtmlElement htmlElement)
        {
            base.CopyProperties(htmlElement);

            if (!htmlElement.Elements.Any())
                return;

            var titleElement = htmlElement.Elements.FirstOrDefault(el => el.Name.ToLower().Equals(ParsingRules.SignificantHtmlTagNames.title));
            if (titleElement != null)
                this.Title = titleElement.Value;
        }
    }
}
