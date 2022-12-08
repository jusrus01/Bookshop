using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Bookshop.BusinessLogic.Services;
using Bookshop.Contracts.DataTransferObjects.Orders;
using Bookshop.Contracts.DataTransferObjects.Users;
using Bookshop.Contracts.Services;
using Bookshop.WebApp.PageModels;
using Bookshop.WebApp.PageModels.Users;
using Bookshop.WebApp.ViewModels.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bookshop.WebApp.Pages.Orders
{
    public class AddModel : BookshopPageModel
    {
        [BindProperty]
        public OrderViewModel OrderInput { get; set; } = new();

        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public AddModel(IOrderService orderService, IMapper mapper, INotyfService notyfService) : base(notyfService)
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
    }
}
