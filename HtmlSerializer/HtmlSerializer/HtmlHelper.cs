using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HtmlSerializer
{
    internal class HtmlHelper
    {
        private readonly static HtmlHelper _instance = new HtmlHelper();

        public static HtmlHelper Instance => _instance;

        public string[] HtmlTags { get; set; }
        public string[] UnclosedTags { get; set; }

        private HtmlHelper()
        {
            string content = File.ReadAllText("./tags-files/html-tags.json");
            HtmlTags = JsonSerializer.Deserialize<string[]>(content);
            content = File.ReadAllText("./tags-files/html-tags-void.json");
            UnclosedTags = JsonSerializer.Deserialize<string[]>(content);
        }
    }
}
