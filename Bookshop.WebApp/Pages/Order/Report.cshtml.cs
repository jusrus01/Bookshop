using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Bookshop.Contracts.DataTransferObjects.Books;
using Bookshop.Contracts.DataTransferObjects.Orders;
using Bookshop.Contracts.Enums;
using Bookshop.Contracts.Services;
using Bookshop.WebApp.PageModels;
using Bookshop.WebApp.ViewModels.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Diagnostics;

namespace Bookshop.WebApp.Pages.Orders
{
    public class OrderReportModel : BookshopPageModel
    {
        public OrderViewModel OrderInput { get; set; } = new();
        private readonly IOrderService _orderService;
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public OrderReportModel(IOrderService orderService, IMapper mapper, INotyfService notyfService) : base(notyfService)
        {
            _orderService = orderService;
            _mapper = mapper;
        }


        public List<BookDto> _books { get; set; }

        public async Task<IActionResult> OnGetAsync(int orderId)
        {
            var order = await _orderService.GetOrderById(orderId);
            OrderInput = _mapper.Map<OrderViewModel>(order);

            List<OrderBookDto> booksByOrder = _orderService.GetAllBooks().Where(book => book.OrderId == OrderInput.Id).ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int orderId)
        {
            var order = await _orderService.GetOrderById(orderId);
            OrderInput = _mapper.Map<OrderViewModel>(order);

            List<OrderBookDto> booksByOrder = _orderService.GetAllBooks().Where(book => book.OrderId == OrderInput.Id).ToList();

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Invoice";
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Arial", 20, XFontStyle.Bold);
            XFont items = new XFont("Arial", 12);

            gfx.DrawString("Invoice", font, XBrushes.Black, new XPoint(180, 100));
            int x_point = 100;
            int y_point = 200;

            for (int i = 0; i < booksByOrder.Count(); i++)
            {
                var book = booksByOrder[i];
                gfx.DrawString(String.Format("{0}. {1}. {2}", i + 1, book.Title, book.Price), items, XBrushes.Black,
                new XPoint(x_point, y_point));
                y_point += 15;
            }

            gfx.DrawString(DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day, font, XBrushes.Black, new XRect(0, 0, page.Width, page.Height), XStringFormats.BottomCenter);
            string filename = @"Pages\Order\Reports\report.pdf";
            document.Save(filename);
            Process.Start(new ProcessStartInfo { FileName = filename, UseShellExecute = true, CreateNoWindow = false });

            return RedirectToPage("List");
        }
    }
}
