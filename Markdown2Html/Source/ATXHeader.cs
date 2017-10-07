using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markdown2Html
{
    class ATXHeader : Parser
    {
        private bool _foundHeader = false;
        private int _headerCount;

        private bool IsHeaderToken(string token)
        {
            return token == "#";
        }

        private bool IsEndTag(string token, DocumentDetails details)
        {
            return MarkdownUtils.IsNewline(details.CurrentIndex, details.OriginalDocument) || details.CurrentIndex == details.OriginalDocument.Length;
        }

        public void OnCharParse(ref string inputCharacter, DocumentDetails details)
        {
            if (IsEndTag(inputCharacter, details) && _foundHeader)
            {
                _foundHeader = false;
                string headerCountStr = Convert.ToString(_headerCount);

                inputCharacter = "</h" + headerCountStr + ">";
            }
            if (IsHeaderToken(inputCharacter))
            {
                int headerCount = 1;
                string currTok = inputCharacter;
                int index = details.CurrentIndex;
                while (index < details.OriginalDocument.Length - 1)
                {
                    index++;
                    currTok = details.OriginalDocument[index].ToString();
                    if (IsHeaderToken(currTok))
                    {
                        headerCount++;
                        details.CurrentIndex++;
                    } else
                    {
                        break;
                    }
                }
                if (headerCount <= 6)
                {
                    string headerCountStr = Convert.ToString(headerCount);
                    _headerCount = headerCount;
                    _foundHeader = true;
                    inputCharacter = "<h" + headerCountStr + ">";
                } else
                {
                    string outputString = "";
                    for (int i = 0; i < headerCount; i++) outputString += "#";
                    inputCharacter = outputString;
                }

            }
        }
    }
}
