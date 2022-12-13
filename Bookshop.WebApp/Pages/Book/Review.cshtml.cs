using Bookshop.Contracts.Constants;
using Bookshop.Contracts.DataTransferObjects.Books;
using Bookshop.Contracts.Services;
using Bookshop.WebApp.Attributes;
using Bookshop.WebApp.PageModels;
using Microsoft.AspNetCore.Mvc;

namespace Bookshop.WebApp.Pages.Book
{
    [AuthorizeAnyOfTheRoles(BookshopRoles.Administrator, BookshopRoles.Manager, BookshopRoles.Client)]
    public class ReviewModel : BookshopPageModel
    {
        private readonly IBookService _bookService;

        public BookDto Book { get; set; }

        public ReviewModel(IBookService bookService) : base(null)
        {
            _bookService = bookService;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            Book = await _bookService.GetBookAsync(id);
            if (Book == null)
            {
                return RedirectToPage("/notFound");
            }
            return Page();
        }
    }
}
