using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Markdown2Html
{
    class Link : Parser
    {
        private bool IsLinkStart(string token)
        {
            return token == "[";
        }

        private bool IsLinkEnd(string token)
        {
            return token == ")";
        }

        public void OnCharParse(ref string inputCharacter, DocumentDetails details)
        {
            bool parsingImage = false;
            if(inputCharacter == "!")
            {
                if(details.CanLookAhed)
                {
                    string advance = details.OriginalDocument[details.CurrentIndex + 1].ToString();
                    if (advance == "[") details.CurrentIndex += 1;
                    parsingImage = true;
                }
            }

            if(IsLinkStart(inputCharacter) || parsingImage)
            {
                string linkMardown = inputCharacter;
                string tok = inputCharacter;
                while(details.CurrentIndex < details.OriginalDocument.Length - 1)
                {
                    details.CurrentIndex++;
                    linkMardown += details.OriginalDocument[details.CurrentIndex];
                    tok = details.OriginalDocument[details.CurrentIndex].ToString();
                    if (IsLinkEnd(tok))
                    {
                        break;
                    }
                }

                string url = Regex.Match(linkMardown, "\\(.*\\s+").Value.Replace("(", "").Replace(" ", "");
                string title = Regex.Match(linkMardown, "\".*\"").Value.Replace("\"", "");

                StringBuilder htmlATag = new StringBuilder();
                if(parsingImage)
                {
                    string linkText = Regex.Match(linkMardown, "\\!.*\\]").Value.Replace("!", "").Replace("]", "");
                    htmlATag.AppendFormat("<img src=\"{0}\" title=\"{1}\" alt=\"{2}\"></img>", url, title, linkText);
                }
                else
                {
                    string linkText = Regex.Match(linkMardown, "\\[.*\\]").Value.Replace("[", "").Replace("]", "");
                    htmlATag.AppendFormat("<a href=\"{0}\" title=\"{1}\">{2}</a>", url, title, linkText);
                }
                string html = htmlATag.ToString();
                inputCharacter = html;
            }
        }
    }
}
