using Bookshop.Contracts;
using Bookshop.Contracts.Constants;
using Bookshop.Contracts.Services;
using Bookshop.WebApp.Attributes;
using Bookshop.WebApp.PageModels;
using Microsoft.AspNetCore.Mvc;

namespace Bookshop.WebApp.Pages.Book
{
    [AuthorizeAnyOfTheRoles(BookshopRoles.Administrator)]
    public class AddModel : BookshopPageModel
    {
        private readonly IUnitOfWork _uow;
        private readonly IBookService _bookService;

        public AddModel(IBookService bookService, IUnitOfWork uow) : base(null)
        {
            _bookService = bookService;
            _uow = uow;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        //[BindProperty]
        //public BookDto Book { get; set; }

        //public async Task<IActionResult> OnPostAsync()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }
        //    var book = await _context.SingleOrDefaultAsync(b => b.Id == id);
        //    _context.Add(Book);

        //    await _uow.SaveChangesAsync();

        //    return RedirectToPage("./Index");
        //}
    }
}