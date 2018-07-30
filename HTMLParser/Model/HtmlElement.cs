using HTMLParser.Parser.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HTMLParser.Model
{
    /// <summary>
    /// Base Html Element.
    /// </summary>
    public class HtmlElement
    {
        public string Value { get; set; }
        public string Name { get; protected set; }
        public List<HtmlAttribute> Attributes { get; protected set; }
        public HtmlElement Parent { get; protected set; }
        public List<HtmlElement> Elements { get; protected set; }
        public bool SelfClosed { get; set; }

        public virtual void SetParent(HtmlElement htmlElement)
        {
            if (Parent != null)
            {
                if (Parent.Elements.Contains(this))
                    Parent.Elements.Remove(this);
            }

            Parent = htmlElement;

            if (!Parent.Elements.Contains(this))
                Parent.Elements.Add(this);
        }

        public virtual void AddElement(HtmlElement htmlElement)
        {
            htmlElement.SetParent(this);
        }

        public virtual void RemoveElement(HtmlElement htmlElement)
        {
            if (Elements.Contains(htmlElement))
                Elements.Remove(htmlElement);
        }

        public HtmlElement()
        {
            this.Elements = new List<HtmlElement>();
            this.Attributes = new List<HtmlAttribute>();
        }

        public HtmlElement(string name) : this()
        {
            this.Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

        protected virtual void CopyProperties(HtmlElement htmlElement)
        {
            this.Attributes = htmlElement.Attributes;
            this.Elements = htmlElement.Elements;
            this.Parent = htmlElement.Parent;
            this.Value = htmlElement.Value;
        }

        internal virtual string GenerateTag()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<" + Name);
            
            foreach(var attribute in Attributes)
                builder.Append(" " + attribute.Name + "=\"" + attribute.Value + "\"");

            if (SelfClosed && !ParsingRules.HtmlTagsForbiddenToBeClosed.Contains(Name))
                builder.Append("/>");
            else
                builder.Append(">");

            return builder.ToString();
        }

        internal virtual string GenerateClosingTag()
        {
            return "</" + Name + ">";
        }
    }
}
