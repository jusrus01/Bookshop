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

namespace Bookshop.WebApp.Pages.Orders
{
    public class AddModel : BookshopPageModel
    {
        [BindProperty]
        public List<OrderBookDto2> SelectedBooks { get; set; } = new();

        [BindProperty]
        public CreateOrderViewModel OrderInput { get; set; } = new();

        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public List<SelectListItem> Books { get; set; }
        public List<SelectListItem> Payments { get; set; }

        public List<SelectListItem> OrderMethods { get; set; }

        public AddModel(IOrderService orderService, IMapper mapper, INotyfService notyfService) : base(notyfService)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        public async Task<IActionResult> OnGetSearchAsync(string term) =>
            new JsonResult(await _orderService.GetBooksForAutocomplete(term));

        // Please do not look at this :)
        public async Task<IActionResult> OnPostUpdateSelectedBooksAsync(
            string bookName,
            string[] selectedBookNames,
            int[] selectedBookIds,
            double[] prices)
        {
            var book = await _orderService.GetBookByNameAsync(bookName);
            if (book != null && selectedBookIds.Contains(book.Id))
            {
                book = null;
            }

            for (var i = 0; i < selectedBookIds.Length; i++)
            {
                SelectedBooks.Add(new OrderBookDto2
                {
                    Id = selectedBookIds[i],
                    Name = selectedBookNames[i],
                    Price = prices[i]
                });
            }
            
            if (book != null)
            { 
                SelectedBooks.Add(book);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAddAsync(
            string clientComment,
            OrderMethod orderMethod,
            PaymentMethod paymentMethod,
            string address,
            string postalCode,
            int[] selectedBookIds,
            double[] prices)
        {
            if (prices == null || prices.Length == 0)
            {
                return PageWithError("Cannot create order without any books");
            }

            try
            {
                var books = new List<OrderBookDto2>();
                for (var i = 0; i < prices.Length; i++)
                {
                    books.Add(new OrderBookDto2
                    {
                        Id = selectedBookIds[i],
                        Price = prices[i]
                    });
                }

                await _orderService.AddAsync(new CreateOrderDto
                {
                    ClientCommentForCourier = clientComment,
                    SelectedBooks = books,
                    DeliveryMethod = orderMethod,
                    PaymentMethod = paymentMethod,
                    Address = address,
                    PostalCode = postalCode
                });
            }
            catch (Exception e)
            {
                return PageWithError(e.Message);
            }

            return RedirectToPage("List");
        }
    }
}
