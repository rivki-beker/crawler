using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlSerializer
{
    internal class Selector
    {
        public string TagName { get; set; }

        public string Id { get; set; }

        public List<string> Classes { get; set; }

        public Selector Parent { get; set; }

        public Selector Child { get; set; }

        public Selector(string query, Selector parent)
        {
            int iId = query.IndexOf("#");
            int iClass = query.IndexOf('.');
            if (query[0] != '#' && query[0] != '.')
            {
                int minI = iId != -1 ? iClass != -1 ? Math.Min(iId, iClass) : iId : iClass;
                string tagName = query.Substring(0, minI != -1 ? minI : query.Length);
                if (HtmlHelper.Instance.HtmlTags.Contains(tagName))
                    this.TagName = tagName;
                query = query.Substring(minI != -1 ? minI : query.Length);
            }
            if (iId != -1)
            {
                iId = query.IndexOf('#');
                iClass = query.IndexOf('.', iId);
                Id = query.Substring(iId + 1, (iClass != -1 ? iClass : query.Length) - iId - 1);
                query = query.Substring(0, iId) + query.Substring(iClass != -1 ? iClass : query.Length);
            }
            if (query.Length > 0)
                Classes = query.Substring(1).Split('.').ToList();
            this.Parent = parent;
        }

        public static Selector ToSelectorElement(string query)
        {
            string[] parts = query.Split(' ');
            Selector rootSelector = new Selector(parts[0], null);
            Selector prev = rootSelector;
            parts = parts.ToList().GetRange(1, parts.Length - 1).ToArray();
            foreach (string part in parts)
            {
                prev.Child = new Selector(part, prev);
                prev = prev.Child;
            }
            return rootSelector;
        }

        public override bool Equals(object? obj)
        {
            if (obj is HtmlElement)
            {
                HtmlElement element = (HtmlElement)obj;
                if ((this.Id == null || element.Id == this.Id) && (this.TagName == null || element.Name == this.TagName))
                {
                    if (this.Classes != null)
                    {
                        if (element.Classes == null)
                            return false;
                        foreach (var c in this.Classes)
                        {
                            if (!element.Classes.Contains(c))
                                return false;
                        }
                    }
                    return true;
                }
            }
            return false;
        }
    }
}
