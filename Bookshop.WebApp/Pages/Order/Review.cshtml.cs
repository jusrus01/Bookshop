using AspNetCoreHero.ToastNotification.Abstractions;
using Bookshop.Contracts.Constants;
using Bookshop.Contracts.DataTransferObjects.Orders;
using Bookshop.Contracts.Services;
using Bookshop.WebApp.Attributes;
using Bookshop.WebApp.PageModels;
using Microsoft.AspNetCore.Mvc;

namespace Bookshop.WebApp.Pages.Order
{
    [AuthorizeAnyOfTheRoles(BookshopRoles.Administrator, BookshopRoles.Manager, BookshopRoles.Client)]
    public class OrderViewModel : BookshopPageModel
    {
        private readonly IOrderService _orderService;

        [BindProperty]
        public OrderDto2 Order { get; set; }
      

        public OrderViewModel(IOrderService orderService, INotyfService notyfService) : base(notyfService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            Order = await _orderService.GetOrderAsync(id);
            return Page();
        }
    }
}
