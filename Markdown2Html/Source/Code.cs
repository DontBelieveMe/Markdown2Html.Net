using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markdown2Html
{
    public class Code : Parser
    {
        private enum ParseType
        {
            Block, Inline, None
        }

        private ParseType _parsing = ParseType.None;

        private bool IsCodeToken(string token)
        {
            return token == "`";
        }

        public void OnCharParse(ref string inputCharacter, DocumentDetails details)
        {
            if(IsCodeToken(inputCharacter))
            {
                if (_parsing == ParseType.None)
                {
                    int numOfTicks = 1;
                    int index = details.CurrentIndex + 1;
                    while (index < details.OriginalDocument.Length - 1)
                    {
                        if (IsCodeToken(details.OriginalDocument[index].ToString()))
                        {
                            details.CurrentIndex++;
                            numOfTicks++;
                            index++;
                        }
                        else break;
                    }

                    if (numOfTicks == 1)
                    {
                        _parsing = ParseType.Inline;
                        inputCharacter = "<code>";
                    }
                    else if (numOfTicks == 3)
                    {
                        _parsing = ParseType.Block;
                        inputCharacter = "<pre><code>";
                    }
                } else
                {
                    if(_parsing == ParseType.Block)
                    {
                        details.CurrentIndex += 2;
                        inputCharacter = "</code></pre>";
                    } else
                    {
                        inputCharacter = "</code>";
                    }
                    _parsing = ParseType.None;
                }
            }
        }
    }
}
