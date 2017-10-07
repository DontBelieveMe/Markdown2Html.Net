using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Markdown2Html
{
    public class Markdown2Html
    {
        private string _markdown;
        private List<Parser> _parsers = new List<Parser>();

        public Markdown2Html(string markdown)
        {
            _markdown = markdown;

            _parsers.Add(new Emphasis());
            _parsers.Add(new Strikethrough());
            _parsers.Add(new ATXHeader());
            _parsers.Add(new Link());
            _parsers.Add(new Code());
            _parsers.Add(new Lists());
            _parsers.Add(new ThematicBreak());
        }

        private bool CanLookAhed(string text, int index)
        {
            return index < text.Length - 1;
        }

        public string GenerateHTMLText()
        {
            return ParseString(_markdown);
        }

        public string ParseString(string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return string.Empty;

            string html = "";
            for (int i = 0; i < str.Length; i++)
            {
                DocumentDetails details = new DocumentDetails(html, CanLookAhed(str, i), i, str, this);
                string charToProcess = str[i].ToString();
                foreach (Parser parser in _parsers)
                {
                    parser.OnCharParse(ref charToProcess, details);
                    i = details.CurrentIndex;
                }
                html += charToProcess;
            }
            return html;
        }
    }
}
