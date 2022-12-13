using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Bookshop.BusinessLogic.Services;
using Bookshop.Contracts.DataTransferObjects.Books;
using Bookshop.Contracts.DataTransferObjects.Orders;
using Bookshop.Contracts.DataTransferObjects.Users;
using Bookshop.Contracts.Enums;
using Bookshop.Contracts.Services;
using Bookshop.DataLayer.Models;
using Bookshop.WebApp.PageModels;
using Bookshop.WebApp.PageModels.Users;
using Bookshop.WebApp.ViewModels.Books;
using Bookshop.WebApp.ViewModels.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace Bookshop.WebApp.Pages.Orders
{
    public class EditModel : BookshopPageModel
    {
        [BindProperty]
        public OrderViewModel OrderInput { get; set; } = new();

        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public List<SelectListItem> Books { get; set; }
        public List<SelectListItem> Payments { get; set; }

        public List<SelectListItem> OrderMethods { get; set; }

        public EditModel(IOrderService orderService, IMapper mapper, INotyfService notyfService) : base(notyfService)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _orderService.AddAsync(_mapper.Map<OrderDto>(OrderInput));
            }
            catch (Exception e)
            {
                return PageWithError(e.Message);
            }

            return RedirectToPage("List");
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            OrderDto order = await _orderService.GetOrderAsync(id);
            OrderInput = new OrderViewModel
            {
                Id = id,
                Sum = order.Sum,
                PostalCode = order.PostalCode,
                Address = order.Address,
                ClientComment = order.ClientComment,
                OrderMethod = order.OrderMethod,
                PaymentMethod = order.PaymentMethod,
                ExpectedDelivery = order.ExpectedDelivery,
                PaymentDate = order.PaymentDate,
                Status = order.Status,
                UserId = order.UserId,
                BookId = order.BookId
            };

            if (_orderService.GetOrderAsync(id) == null)
            {
                return RedirectToPage("/notFound");
            }

            _orderService.GetOrderAsync(id);

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
            return Page();
        }
    }
}
