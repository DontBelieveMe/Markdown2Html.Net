using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markdown2Html
{
    public class ThematicBreak : Parser
    {
        private bool IsThematicBreakToken(string token)
        {
            return token == "*" || token == "-" || token == "_";
        }

        public void OnCharParse(ref string inputCharacter, DocumentDetails details)
        {
            if(IsThematicBreakToken(inputCharacter))
            {
                int num = 1;
                int index = details.CurrentIndex;
                while(details.CurrentIndex < details.OriginalDocument.Length - 1)
                {
                    index++;

                    if (!IsThematicBreakToken(details.OriginalDocument[index].ToString()))
                        break;

                    details.CurrentIndex++;
                    num++;
                }

                if(num >= 3)
                {
                    inputCharacter = "<hr />";
                }
            }
        }
    }
}
