using HTMLParser.Helpers;
using HTMLParser.Parser;
using HTMLParser.Parser.Common;
using HTMLParser.Parser.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTMLParser.Model
{
    /// <summary>
    /// Html document which represents HTML in a tree structure.
    /// </summary>
    public class HtmlDocument
    {
        public HeadHtmlElement Head { get; protected set; }
        public BodyHtmlElement Body { get; protected set; }
        private HtmlElement root;
        public HtmlElement Root
        {
            get
            {
                return root;
            }
            set
            {
                if (value == null)
                    return;

                this.root = value;
                var headElement = root.Elements.FirstOrDefault(el => el.Name.Equals(ParsingRules.SignificantHtmlTagNames.head));
                if (headElement != null)
                    this.Head = new HeadHtmlElement(headElement);
                else
                    this.Head = new HeadHtmlElement();

                var bodyElement = root.Elements.FirstOrDefault(el => el.Name.Equals(ParsingRules.SignificantHtmlTagNames.body));
                if (bodyElement != null)
                    this.Body = new BodyHtmlElement(bodyElement);
                else
                    this.Body = new BodyHtmlElement();
            }
        }
        public string Title
        {
            get
            {
                return Head?.Title;
            }
        }

        internal HtmlDocument(HtmlElement root)
        {
            this.Root = root;
        }

        public static HtmlDocument Load(string html)
        {
            ParsingManager parsingManger = new ParsingManager();
            var document = parsingManger.Parse(html);
            return document;
        }

        public override string ToString()
        {
            if (root == null)
                return string.Empty;

            StringBuilder htmlBuilder = new StringBuilder();
            GenerateHtml(htmlBuilder, -1, new List<HtmlElement> { this.root });
            return htmlBuilder.ToString();
        }

        protected void GenerateHtml(StringBuilder stringBuilder, int level, List<HtmlElement> htmlElements)
        {
            level++;
            foreach(var htmlElement in htmlElements)
            {
                stringBuilder.Append(StringHelper.GenerateTabs(level) + htmlElement.GenerateTag());
                if (!htmlElement.SelfClosed)
                {
                    if (!string.IsNullOrEmpty(htmlElement.Value))
                        stringBuilder.AppendLine(htmlElement.Value);
                    else if (htmlElement.Elements.Any())
                        stringBuilder.AppendLine();

                    if (htmlElement.Elements.Any())
                        GenerateHtml(stringBuilder, level, htmlElement.Elements);

                    stringBuilder.AppendLine(StringHelper.GenerateTabs(level) + htmlElement.GenerateClosingTag());
                }
                else
                    stringBuilder.AppendLine();
            }
        }
    }
}
