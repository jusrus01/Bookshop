using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Bookshop.Contracts.Constants;
using Bookshop.Contracts.DataTransferObjects.Books;
using Bookshop.Contracts.Services;
using Bookshop.WebApp.Attributes;
using Bookshop.WebApp.PageModels;
using Bookshop.WebApp.ViewModels.Books;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bookshop.WebApp.Pages.Book
{
    [AuthorizeAnyOfTheRoles(BookshopRoles.Manager, BookshopRoles.Administrator, BookshopRoles.Client)]
    public class EditModel : BookshopPageModel
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        [BindProperty]
        public BookViewModel Book { get; set; }

        public List<SelectListItem> Genres { get; set; }
        public List<SelectListItem> Suppliers { get; set; }

        public EditModel(IBookService bookService, IMapper mapper, INotyfService notyfService) : base(notyfService)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            BookDto book = await _bookService.GetBookAsync(id);
            Book = new BookViewModel
            {
                Id = id,
                ISBN = book.ISBN,
                Title = book.Title,
                Author = book.Author,
                Year = book.Year,
                Pages = book.Pages,
                Description = book.Description,
                Price = book.Price,
                AddedDate = book.AddedDate,
                Discount = book.Discount,
                Supplier = book.Supplier,
                Genre = book.Genre
            };

            if (await _bookService.GetBookAsync(id) == null)
            {
                return RedirectToPage("/notFound");
            }

            List<GenreDto> genres = await _bookService.GetGenres();
            List<SupplierDto> suppliers = await _bookService.GetSupplier();

            List<SelectListItem> genreList = new List<SelectListItem>();
            List<SelectListItem> supplierList = new List<SelectListItem>();

            for (int i = 0; i < genres.Count; i++)
            {
                genreList.Add(new SelectListItem { Text = genres[i].Name, Value = genres[i].Id.ToString() });
            }

            for (int i = 0; i < suppliers.Count; i++)
            {
                supplierList.Add(new SelectListItem { Text = suppliers[i].Name, Value = suppliers[i].Id.ToString() });
            }

            this.Genres = genreList;
            this.Suppliers = supplierList;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _bookService.UpdateAsync(_mapper.Map<BookDto>(Book));
            }
            catch (Exception e)
            {
                return await PageWithErrorAsync(e.Message, () => OnGetAsync(Book.Id));
            }

            return RedirectToPage("List");
        }
    }
}