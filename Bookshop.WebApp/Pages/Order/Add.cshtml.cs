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

        public async void OnGet()
        {
            List<BookDto> books = await _orderService.GetBooks();
            List<SelectListItem> bookList = new List<SelectListItem>();

            List<PaymentMethod> payments = Enum.GetValues(typeof(PaymentMethod)).Cast<PaymentMethod>().ToList();
            List<SelectListItem> paymentsList = new List<SelectListItem>();

            List<OrderMethod> ordersMethods = Enum.GetValues(typeof(OrderMethod)).Cast<OrderMethod>().ToList();
            List<SelectListItem> ordersMethodsList = new List<SelectListItem>();

            for (int i = 0; i < books.Count; i++)
            {
                bookList.Add(new SelectListItem { Text = books[i].Title, Value = books[i].Id.ToString() });
            }

            for (int i = 0; i < payments.Count; i++)
            {
                paymentsList.Add(new SelectListItem { Text = payments[i].ToString(), Value = payments[i].ToString() });
            }

            for (int i = 0; i < ordersMethods.Count; i++)
            {
                ordersMethodsList.Add(new SelectListItem { Text = ordersMethods[i].ToString(), Value = ordersMethods[i].ToString() });
            }

            this.Books = bookList;
            this.Payments = paymentsList;
            this.OrderMethods = ordersMethodsList;
        }
    }
}
