using PdfSharpCore.Drawing;
using PdfSharpCore.Fonts;
using PdfSharpCore.Pdf;
using PdfSharpCore.Utils;

namespace Bookshop.BusinessLogic.Builders
{
    public class InternalPdfPage
    {
        private readonly PdfDocument _document;
        private readonly PdfPage _page;
        private readonly XGraphics _gfx;

        private readonly XSolidBrush _color;
        private readonly XRect _layout;
        private readonly XStringFormat _format;
        private readonly XFont _font;

        public InternalPdfPage(PdfDocument document, PdfPage page)
        {
            _document = document;
            _page = page;
            _gfx = XGraphics.FromPdfPage(page);
            _color = XBrushes.Black;
            _layout = new XRect(20, 20, page.Width, page.Height);
            _format = XStringFormats.Center;
            _font = new XFont("Arial", 20, XFontStyle.Bold);
        }

        public InternalPdfPage AddString(string s)
        {
            _gfx.DrawString(s, _font, _color, _layout, _format);
            return this;
        }

        public PdfBuilder Finish()
        {
            return new PdfBuilder(_document);
        }
    }

    public class PdfBuilder
    {
        private readonly PdfDocument _document;

        public PdfBuilder(PdfDocument document)
        {
            GlobalFontSettings.FontResolver = new FontResolver();
            _document = document;
        }

        public PdfBuilder()
        {
            GlobalFontSettings.FontResolver = new FontResolver();
            _document = new PdfDocument();
        }

        public InternalPdfPage AddPage()
        {
            var page = _document.AddPage();
            return new InternalPdfPage(_document, page);
        }

        public byte[] Build()
        {
            using var memory = new MemoryStream();
            _document.Save(memory, false);
            _document.Close();
            return memory.ToArray();
        }
    }
}
