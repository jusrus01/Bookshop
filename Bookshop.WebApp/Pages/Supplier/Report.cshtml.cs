using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Bookshop.Contracts.Constants;
using Bookshop.Contracts.DataTransferObjects.Books;
using Bookshop.Contracts.DataTransferObjects.Suppliers;
using Bookshop.Contracts.Services;
using Bookshop.DataLayer.Models;
using Bookshop.WebApp.Attributes;
using Bookshop.WebApp.PageModels;
using Bookshop.WebApp.ViewModels.Suppliers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
//for pdf
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Diagnostics;
using System.Drawing;

namespace Bookshop.WebApp.Pages.Supplier
{
    [AuthorizeAnyOfTheRoles(BookshopRoles.Manager, BookshopRoles.Client, BookshopRoles.Administrator)]
    public class ReportModel : BookshopPageModel
    {
        private readonly ISupplierService _supplierService;
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;
        public ReportModel(ISupplierService supplierService, IBookService bookService, IMapper mapper, INotyfService notyfService) : base(notyfService)
        {
            _supplierService = supplierService;
            _bookService = bookService;
            _mapper = mapper;
        }

        public SupplierViewModel _supplier { get; set; }
        public List<GenreDto> _mostPopularGenres { get; set; }
        public List<int> _mostPopularGenresCount { get; set; }
        public async Task<IActionResult> OnGetAsync(int supplierId)
        {
            var supplier = await _supplierService.GetSupplierById(supplierId);
            _supplier = _mapper.Map<SupplierViewModel>(supplier);

            List<SupplierBookDto> booksBySupplier = _supplierService.GetAllBooks().Where(book => book.SupplierId == _supplier.Id).ToList();
            List<GenreDto> allGenres = _supplierService.GetAllGenres();
            List<int> genreCount = new List<int>(new int[allGenres[allGenres.Count() - 1].Id + 1]);
            for (int i = 0; i < booksBySupplier.Count; i++)
            {
                genreCount[booksBySupplier[i].GenreId]++;
            }
            _mostPopularGenres = allGenres.OrderByDescending(genre => genreCount[genre.Id]).ToList();
            _mostPopularGenresCount = genreCount.OrderByDescending(i => i).ToList();

            return Page();

        }

        public async Task<IActionResult> OnPostAsync (int supplierId, int mostPopularGenresAmount)
        {
            var supplier = await _supplierService.GetSupplierById(supplierId);
            _supplier = _mapper.Map<SupplierViewModel>(supplier);
            List<SupplierBookDto> booksBySupplier = _supplierService.GetAllBooks().Where(book => book.SupplierId == _supplier.Id).ToList();
            List<GenreDto> allGenres = _supplierService.GetAllGenres();
            List<int> genreCount = new List<int>(new int[allGenres[allGenres.Count() - 1].Id + 1]);
            for (int i = 0; i < booksBySupplier.Count; i++)
            {
                genreCount[booksBySupplier[i].GenreId]++;
            }
            _mostPopularGenres = allGenres.OrderByDescending(genre => genreCount[genre.Id]).ToList();
            _mostPopularGenresCount = genreCount.OrderByDescending(i => i).ToList();
            int totalSum = _mostPopularGenresCount.Sum();

            PdfDocument document = new PdfDocument();
            document.Info.Title = "Most Popular Genres Report";
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Arial", 20, XFontStyle.Bold);
            XFont font_lower = new XFont("Arial", 16);
            XFont items = new XFont("Arial", 12);
            gfx.DrawString("BOOKSHOP", font, XBrushes.Black,
            new XPoint(240, 50));
            gfx.DrawString("Most popular genres report", font_lower, XBrushes.Black,
            new XPoint(200, 100));
            gfx.DrawLine(new XPen(new XColor() { R = 0, G = 0, B = 0}),
            new XPoint(100, 110), new XPoint(500,110));
            int x_point = 100;
            int y_point = 130;
            for (int i = 0; i < _mostPopularGenres.Count() && i < mostPopularGenresAmount; i++)
            {
                var genre = _mostPopularGenres[i];
                gfx.DrawString(String.Format("{0}. Genre: {1}. Percentage: {2:0.00}%", i + 1, genre.Name, ((float)(_mostPopularGenresCount[i] * 1.00 / (totalSum > 0 ? totalSum : 1))* 100)), items, XBrushes.Black,
                new XPoint(x_point, y_point));
                y_point += 15;
            }
            gfx.DrawLine(new XPen(new XColor() { R = 0, G = 0, B = 0 }),
            new XPoint(100, y_point), new XPoint(500, y_point));
            gfx.DrawString(DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day, font, XBrushes.Black,
            new XRect(0, 0, page.Width, page.Height), XStringFormats.BottomCenter);

            using var memory = new MemoryStream();
            document.Save(memory, false);
            document.Close();
            return File(memory.ToArray(), "application/pdf");
        }
    }
}
