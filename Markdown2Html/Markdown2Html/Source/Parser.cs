using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markdown2Html
{
    interface Parser
    {
        void OnCharParse(ref string inputCharacter, DocumentDetails details);
    }
}
