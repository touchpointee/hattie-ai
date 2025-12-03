using System.IO;
using UglyToad.PdfPig;

namespace HattieAI.Infrastructure.Documents
{
    public class DocumentBroker
    {
        public string ExtractText(Stream pdfStream)
        {
            using var document = PdfDocument.Open(pdfStream);
            var text = string.Empty;
            foreach (var page in document.GetPages())
            {
                text += page.Text + "\n";
            }
            return text;
        }
    }
}
