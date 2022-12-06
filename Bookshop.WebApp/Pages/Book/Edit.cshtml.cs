using Bookshop.Contracts.Constants;
using Bookshop.Contracts.Services;
using Bookshop.WebApp.Attributes;
using Bookshop.WebApp.PageModels;
using Microsoft.AspNetCore.Mvc;

namespace Bookshop.WebApp.Pages.Book
{
    [AuthorizeAnyOfTheRoles(BookshopRoles.Manager, BookshopRoles.Administrator)]
    public class EditModel : BookshopPageModel
    {
        private readonly IBookService _bookService;

        public EditModel(IBookService bookService) : base(null)
        {
            _bookService = bookService;
        }

        public IActionResult OnGet(int id)
        {
            if (_bookService.GetBookAsync(id) == null)
            {
                return RedirectToPage("/notFound");
            }

            _bookService.GetBookAsync(id);
            return Page();
        }

    }
}