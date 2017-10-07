using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markdown2Html
{
    public class SetextHeader : Parser
    {
        private bool IsSetextToken(string token)
        {
            return token == "=" || token == "*";
        }

        private int CountTokens(DocumentDetails details)
        {
            int index = details.CurrentIndex;
            int count = 0;
            while (index < details.OriginalDocument.Length - 1)
            {
                if(IsSetextToken(details.OriginalDocument[index].ToString()))
                {
                    count++;
                    index++;
                } else
                {
                    break;
                }
            }
            return count;
        }

        public void OnCharParse(ref string inputCharacter, DocumentDetails details)
        {
            if(IsSetextToken(inputCharacter))
            {
                
            }
        }
    }
}
