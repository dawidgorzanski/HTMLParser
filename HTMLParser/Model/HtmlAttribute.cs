using System;
using System.Collections.Generic;
using System.Text;

namespace HTMLParser.Model
{
    public class HtmlAttribute
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public HtmlAttribute()
        {

        }

        public HtmlAttribute(string name, string value = "")
        {
            this.Name = name;
            this.Value = value;
        }

        public override string ToString()
        {
            return Name + " = \"" + Value + "\"";
        }
    }
}
