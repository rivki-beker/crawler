using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlSerializer
{
    internal static class HtmlElementExtension
    {
        public static HashSet<HtmlElement> SearchBySelector(this HtmlElement element, Selector selector)
        {
            HashSet<HtmlElement> result = new HashSet<HtmlElement>();
            if (selector.Equals(element))
                element.SearchBySelector(selector.Child, result);
            element.SearchBySelector(selector, result);
            return result;
        }

        public static void SearchBySelector(this HtmlElement element, Selector selector, HashSet<HtmlElement> result)
        {
            foreach (var child in element.Descendants())
                if (selector.Equals(child))
                    if (selector.Child == null)
                        result.Add(child);
                    else
                        child.SearchBySelector(selector.Child, result);
        }
    }
}
