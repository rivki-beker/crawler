using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HtmlSerializer
{
    internal class HtmlElement
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public List<string> Attributes { get; set; }

        public List<string> Classes { get; set; }

        public string InnerHtml { get; set; }

        public HtmlElement Parent { get; set; }

        public List<HtmlElement> Children { get; set; }

        public HtmlElement(string name, string attributes, HtmlElement parent)
        {
            Name = name;
            var atrMatch = new Regex("([^\\s]*?)=\"(.*?)\"").Matches(attributes).ToList();
            Attributes = new List<string>();
            foreach (var match in atrMatch)
            {
                string attribute = match.Value;
                string attributeName = attribute.Substring(0, attribute.IndexOf("="));
                string value = attribute.Substring(attribute.IndexOf("\"") + 1);
                value = value.Substring(0, value.LastIndexOf("\""));
                if (attributeName == "id")
                    Id = value;
                else if (attributeName == "class")
                    Classes = value.Split(" ").ToList();
                else
                    Attributes.Add(attribute);
            }
            Parent = parent;
        }

        public IEnumerable<HtmlElement> Descendants()
        {
            Queue<HtmlElement> queue = new Queue<HtmlElement>();
            if (this.Children != null)
                foreach (var child in this.Children)
                    queue.Enqueue(child);
            while (queue.Count > 0)
            {
                HtmlElement element = queue.Dequeue();
                yield return element;
                if (element.Children != null)
                    foreach (var child in element.Children)
                        queue.Enqueue(child);
            }
        }

        public IEnumerable<HtmlElement> Ancestors()
        {
            HtmlElement parentElement = this.Parent;
            while (parentElement != null)
            {
                yield return parentElement;
                parentElement = parentElement.Parent;
            }
        }
    }
}
