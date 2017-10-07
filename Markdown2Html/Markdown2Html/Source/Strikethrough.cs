using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markdown2Html
{
    public class Strikethrough : Parser
    {
        private bool _foundStrikethroughToken;

        private bool IsStrikeThroughToken(string token)
        {
            return token == "~";
        }

        public void OnCharParse(ref string inputCharacter, DocumentDetails details)
        {
            if(IsStrikeThroughToken(inputCharacter))
            {
                if(_foundStrikethroughToken)
                {
                    _foundStrikethroughToken = false;
                    inputCharacter = "</del>";
                }
                else
                {
                    _foundStrikethroughToken = true;
                    inputCharacter = "<del>";
                }
            }
        }
    }
}
