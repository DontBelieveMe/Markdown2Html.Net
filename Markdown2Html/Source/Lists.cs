using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markdown2Html
{
    class Lists : Parser
    {
        private bool IsUnorderedToken(string tok)
        {
            return tok == "*" || tok == "-";
        }

        public void OnCharParse(ref string inputCharacter, DocumentDetails details)
        {
        }
    }
}
