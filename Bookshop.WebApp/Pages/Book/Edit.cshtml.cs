using Bookshop.Contracts;
using Bookshop.Contracts.Constants;
using Bookshop.Contracts.Services;
using Bookshop.DataLayer.Models;
using Bookshop.WebApp.Attributes;
using Bookshop.WebApp.PageModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookshop.WebApp.Pages.Book
{
    [AuthorizeAnyOfTheRoles(BookshopRoles.Manager, BookshopRoles.Administrator)]
    public class EditModel : BookshopPageModel
    {
        private readonly IBookService _bookService;
        private readonly DbSet<Supplier> _supplierDbSet;

        public EditModel(IBookService bookService, IUnitOfWork uow) : base(null)
        {
            _bookService = bookService;
            _supplierDbSet = uow.GetDbSet<Supplier>();
        }

        public EditModel() : base(null)
        {
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

        public List<string> Authors()
        {
            List<string> authors = new List<string>();
            authors.AddRange(_supplierDbSet.GetAsyncEnumerator().Current());

            return authors;
        }
    }
}