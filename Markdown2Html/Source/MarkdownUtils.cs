using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markdown2Html
{
    class MarkdownUtils
    {
        public static bool IsNewline(int index, string text)
        {
            string atIndex = text[index].ToString();
            if (atIndex == "\n") return true;
            if (atIndex == "\r") return true;
            if(index < text.Length - 1)
            {
                if (atIndex == "\r" && text[index + 1].ToString() == "\n") return true;
            }

            return false;
        }
    }
}
