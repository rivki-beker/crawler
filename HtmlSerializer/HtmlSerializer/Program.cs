﻿using HtmlSerializer;
using System.Text.RegularExpressions;

var html = await Load("https://learn.malkabruk.co.il/practicode/projects/pract-2/");

var cleanHtml = new Regex("\\s").Replace(html, " ");

var htmlLines = new Regex("<(.*?)>").Split(cleanHtml).Where(i => i.Length > 0 && i[0] != ' ').ToList();

HtmlElement root = new HtmlElement("html", htmlLines[1].Substring(htmlLines[1].IndexOf(" ") + 1), null);
HtmlElement currentElement = root;

htmlLines = htmlLines.GetRange(2, htmlLines.Count() - 2);

foreach (string line in htmlLines)
{
    if (line[0] == '/')
        if (line.Substring(1) == currentElement.Name)
            currentElement = currentElement.Parent;
        else
        {
            if (currentElement.InnerHtml==null)
                currentElement.InnerHtml = "";
            currentElement.InnerHtml += line;
        }
    else
    {
        var space = line.IndexOf(" ");
        var tagName = line.Substring(0, space == -1 ? line.Length : space);
        if (HtmlHelper.Instance.HtmlTags.Contains(tagName))
        {
            HtmlElement newElement = new HtmlElement(tagName, line.Substring(line.IndexOf(" ") + 1), currentElement);
            if (currentElement.Children == null)
                currentElement.Children = new List<HtmlElement>();
            currentElement.Children.Add(newElement);
            if (!HtmlHelper.Instance.UnclosedTags.Contains(tagName))
                currentElement = newElement;
        }
        else
        {
            if (currentElement.InnerHtml==null)
                currentElement.InnerHtml = "";
            currentElement.InnerHtml += line;
        }
    }
}

var res = root.SearchBySelector(Selector.ToSelectorElement("footer p"));

static async Task<string> Load(string url)
{
    HttpClient client = new HttpClient();
    var response = await client.GetAsync(url);
    var html = await response.Content.ReadAsStringAsync();
    return html;
}

foreach (var ancestor in res.First().Ancestors())
{
    Console.WriteLine(ancestor.Name);
} 