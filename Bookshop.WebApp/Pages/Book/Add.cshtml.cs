using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using Bookshop.Contracts;
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
    public class AddModel : BookshopPageModel
    {
        private readonly IUnitOfWork _uow;
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        [BindProperty]
        public BookViewModel BookInput { get; set; } = new();

        public List<SelectListItem> Genres { get; set; }
        public List<SelectListItem> Suppliers { get; set; }

        public AddModel(
            IBookService bookService,
            IMapper mapper,
            IUnitOfWork uow,
            INotyfService notyfService)
            : 
            base(notyfService)
        {
            _bookService = bookService;
            _mapper = mapper;
            _uow = uow;
        }

        public async void OnGet()
        {
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
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _bookService.AddAsync(_mapper.Map<BookDto>(BookInput));
            }
            catch (Exception e)
            {
                return PageWithError(e.Message);
            }

            return RedirectToPage("List");
        }
    }
}