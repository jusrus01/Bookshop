using AutoMapper;
using Bookshop.Contracts.Constants;
using Bookshop.Contracts.Generics;
using Bookshop.Contracts.Services;
using Bookshop.WebApp.Attributes;
using Bookshop.WebApp.PageModels;
using Bookshop.WebApp.ViewModels.Clients;
using Microsoft.AspNetCore.Mvc;

namespace Bookshop.WebApp.Pages.Book
{
    [AuthorizeAnyOfTheRoles(BookshopRoles.Client, BookshopRoles.Administrator, BookshopRoles.Manager)]
    public class ListModel : SinglePaginationBookshopPagedModel<PartialBookViewModel>
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public ListModel(IBookService bookService, IMapper mapper)
            :
            base(null)
        {
            _mapper = mapper;
            _bookService = bookService;
        }

        public async Task<IActionResult> OnGetAsync(int pageNumber = 1)
        {
            var books = await _bookService.GetBooksPagedAsync(pageNumber, pageSize: 4);

            SetPageItems(_mapper.Map<Paged<PartialBookViewModel>>(books), pageNumber);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _bookService.DeleteBookAsync(id);
            return await OnGetAsync();
        }
    }
}
