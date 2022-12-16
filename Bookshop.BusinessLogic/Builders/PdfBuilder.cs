using PdfSharpCore;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace Bookshop.BusinessLogic.Builders
{
    public class PdfBuilder
    {
        public static byte[] Build(string html)
        {
            var document = PdfGenerator.GeneratePdf(html, PageSize.A4);
            using var memory = new MemoryStream();
            document.Save(memory, false);
            document.Close();
            return memory.ToArray();
        }
    }
}
