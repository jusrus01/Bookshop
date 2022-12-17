using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Bookshop.Contracts;
using Bookshop.Contracts.Constants;
using Bookshop.Contracts.DataTransferObjects.Orders;
using Bookshop.Contracts.Services;
using Bookshop.WebApp.Attributes;
using Bookshop.WebApp.PageModels;
using Bookshop.WebApp.ViewModels.Orders;
using Microsoft.AspNetCore.Mvc;

namespace Bookshop.WebApp.Pages.Orders
{
    [AuthorizeAnyOfTheRoles(BookshopRoles.Administrator, BookshopRoles.Manager)]
    public class EditModel : BookshopPageModel
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderDto2 Order { get; set; }

        public EditModel(IOrderService orderService, INotyfService notyfService, IMapper mapper) : base(notyfService)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        public async Task<IActionResult> OnPostAsync(EditOrderViewModel editOrder)
        {
            await _orderService.UpdateAsync(_mapper.Map<EditOrderDto>(editOrder));
            return RedirectToPage("List");
        }

        public async Task<IActionResult> OnGet(int id)
        {
            Order = await _orderService.GetOrderAsync(id);
            return Page();
        }
    }
}
