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

        public void OnCharParse(ref string inputCharacter, DocumentDetails details)
        {
            if(IsEmhasisToken(inputCharacter))
            {
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
