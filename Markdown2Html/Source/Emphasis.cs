using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markdown2Html
{
    public class Emphasis : Parser
    {
        private bool _foundItallicToken = false;
        private bool _foundStrongToken = false;

        private bool IsEmhasisToken(string token)
        {
            return token == "*" || token == "_";
        }

        private void ParseStrongEmphasisToken(ref string token, DocumentDetails details, out bool foundStrongToken)
        {
            string advance = details.OriginalDocument[details.CurrentIndex + 1].ToString();
            foundStrongToken = false;
            if (IsEmhasisToken(advance))
            {
                foundStrongToken = true;
                details.CurrentIndex += 1;
                if (_foundStrongToken)
                {
                    token = "</strong>";
                    _foundStrongToken = false;

                }
                else
                {
                    token = "<strong>";
                    _foundStrongToken = true;
                }
            }
        }

        private void ParseNormalEmphasisToken(ref string token, DocumentDetails details)
        {
            if (_foundItallicToken)
            {
                _foundItallicToken = false;
                token = "</em>";
            }
            else
            {
                _foundItallicToken = true;
                token = "<em>";
            }
        }

        private bool IsBreak(DocumentDetails details)
        {
            int index = details.CurrentIndex;
            int count = 0;
            while (index < details.OriginalDocument.Length - 1)
            {
                if (IsEmhasisToken(details.OriginalDocument[index].ToString()))
                {
                    count++;
                    index++;
                }
                else break;
            }
            if(count >= 3)
            {
                if (MarkdownUtils.IsNewline(index, details.OriginalDocument)) return true;
            }
            return false;
        }

        public void OnCharParse(ref string inputCharacter, DocumentDetails details)
        {
            if(IsEmhasisToken(inputCharacter))
            {
                bool isBreak = IsBreak(details);
                if (isBreak) return;
                
                // Check if this character is followed by another '*'
                if(details.CanLookAhed)
                {
                    bool foundStrongToken;
                    ParseStrongEmphasisToken(ref inputCharacter, details, out foundStrongToken);
                    if (foundStrongToken) return;

                }

                ParseNormalEmphasisToken(ref inputCharacter, details);
            }
        }
    }
}
