namespace Markdown2Html
{
    public class DocumentDetails
    {
        public string DocumentHTML;
        public bool CanLookAhed;
        public int CurrentIndex;
        public string OriginalDocument;
        public Markdown2Html Parser;
        public string Next;

        public DocumentDetails(string html, bool lookAhed, int currIndex, string orig, Markdown2Html parser)
        {
            DocumentHTML = html;
            CanLookAhed = lookAhed;
            CurrentIndex = currIndex;
            OriginalDocument = orig;
            Parser = parser;
            if(CanLookAhed)
            {
                Next = OriginalDocument[currIndex + 1].ToString();
            }
        }
    }
}