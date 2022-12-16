using AutoMapper;
using Bookshop.Contracts.Constants;
using Bookshop.Contracts.Generics;
using Bookshop.Contracts.Services;
using Bookshop.WebApp.Attributes;
using Bookshop.WebApp.PageModels;
using Bookshop.WebApp.ViewModels.Orders;
using Microsoft.AspNetCore.Mvc;

namespace Bookshop.WebApp.Pages.Order
{
    [AuthorizeAnyOfTheRoles(BookshopRoles.Client, BookshopRoles.Administrator, BookshopRoles.Manager)]
    public class ListModel : SinglePaginationBookshopPagedModel<PartialOrderViewModel>
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        private const int PageSize = 10;

        public ListModel(IOrderService bookService, IMapper mapper)
            :
            base(null)
        {
            _mapper = mapper;
            _orderService = bookService;
        }

        public async Task<IActionResult> OnGetAsync(int pageNumber = 1)
        {
            var books = await _orderService.GetBooksPagedAsync(pageNumber, PageSize);
            SetPageItems(_mapper.Map<Paged<PartialOrderViewModel>>(books), pageNumber);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _orderService.DeleteOrderAsync(id);
            return await OnGetAsync();
        }
    }
}
