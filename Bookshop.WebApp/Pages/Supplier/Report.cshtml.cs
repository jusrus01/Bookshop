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

            PdfDocument document = new PdfDocument();
            document.Info.Title = "Most Popular Genres Report";
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Arial", 20, XFontStyle.Bold);
            XFont items = new XFont("Arial", 12);

            gfx.DrawString("Most popular genres report", font, XBrushes.Black,
            new XPoint(180, 100));
            int x_point = 100;
            int y_point = 200;
            for (int i = 0; i < _mostPopularGenres.Count() && i < mostPopularGenresAmount; i++)
            {
                var genre = _mostPopularGenres[i];
                gfx.DrawString(String.Format("{0}. {1}. Count: {2}", i + 1, genre.Name, _mostPopularGenresCount[i]), items, XBrushes.Black,
                new XPoint(x_point, y_point));
                y_point += 15;
            }
            gfx.DrawString(DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day, font, XBrushes.Black,
            new XRect(0, 0, page.Width, page.Height), XStringFormats.BottomCenter);

            using var memory = new MemoryStream();
            document.Save(memory, false);
            document.Close();
            return File(memory.ToArray(), "application/pdf");
        }
    }
}
