using Bookshop.Contracts.Constants;
using Bookshop.Contracts.DataTransferObjects.Orders;
using Bookshop.Contracts.Services;
using Bookshop.DataLayer.Models;
using Bookshop.WebApp.Attributes;
using Bookshop.WebApp.PageModels;
using Microsoft.AspNetCore.Mvc;

namespace Bookshop.WebApp.Pages.Order
{
    [AuthorizeAnyOfTheRoles(BookshopRoles.Administrator, BookshopRoles.Manager, BookshopRoles.Client)]
    public class OrderViewModel : BookshopPageModel
    {
        private readonly IOrderService _orderService;

        public OrderDto Order { get; set; }
      

        public OrderViewModel(IOrderService orderService) : base(null)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            Order = await _orderService.GetOrderAsync(id);
            if (Order == null)
            {
                return RedirectToPage("/notFound");
            }
            return Page();
        }
    }
}
