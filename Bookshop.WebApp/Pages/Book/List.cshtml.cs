using AutoMapper;
using Bookshop.Contracts.Constants;
using Bookshop.Contracts.DataTransferObjects.Books;
using Bookshop.Contracts.Generics;
using Bookshop.Contracts.Services;
using Bookshop.WebApp.Attributes;
using Bookshop.WebApp.PageModels;
using Bookshop.WebApp.ViewModels.Books;
using Bookshop.WebApp.ViewModels.Clients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bookshop.WebApp.Pages.Book
{
    [AuthorizeAnyOfTheRoles(BookshopRoles.Client, BookshopRoles.Administrator, BookshopRoles.Manager)]
    public class ListModel : SinglePaginationBookshopPagedModel<PartialBookViewModel>
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        [BindProperty]
        public FilterViewModel Filter { get; set; }

        public List<SelectListItem> Authors { get; set; }

        public ListModel(IBookService bookService, IMapper mapper)
            :
            base(null)
        {
            _mapper = mapper;
            _bookService = bookService;
        }

        public async Task<IActionResult> OnGetAsync(int pageNumber = 1)
        {
            List<string> authors = await _bookService.GetAuthor();
            List<SelectListItem> authorsList = new List<SelectListItem>();
            for (int i = 0; i < authors.Count; i++)
            {
                authorsList.Add(new SelectListItem { Text = authors[i].ToString(), Value = authors[i].ToString() });
            }
            this.Authors = authorsList;

            var books = await _bookService.GetBooksPagedAsync(pageNumber, pageSize: 4);

            SetPageItems(_mapper.Map<Paged<PartialBookViewModel>>(books), pageNumber);

            return Page();
        }

        public async Task<IActionResult> OnPostFilterTitleAsync(int pageNumber = 1)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {

                FilterDto filters = new FilterDto
                {
                    Title = Filter.Title
                };

                var books = await _bookService.GetBooksByTitlePagedAsync(pageNumber, pageSize: 10, filters);

                SetPageItems(_mapper.Map<Paged<PartialBookViewModel>>(books), pageNumber);

                List<string> authors = await _bookService.GetAuthor();
                List<SelectListItem> authorsList = new List<SelectListItem>();
                for (int i = 0; i < authors.Count; i++)
                {
                    authorsList.Add(new SelectListItem { Text = authors[i].ToString(), Value = authors[i].ToString() });
                }
                this.Authors = authorsList;

                return Page();
            }
            catch (Exception e)
            {
                return PageWithError(e.Message);
            }
        }

        public async Task<IActionResult> OnPostFilterDiscountAsync(int pageNumber = 1)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                FilterDto filters = new FilterDto
                {
                    MinDicount = Filter.MinDiscount,
                    MaxDicount = Filter.MaxDiscount
                };

                var books = await _bookService.GetBooksByDiscountPagedAsync(pageNumber, pageSize: 10, filters);

                SetPageItems(_mapper.Map<Paged<PartialBookViewModel>>(books), pageNumber);

                List<string> authors = await _bookService.GetAuthor();
                List<SelectListItem> authorsList = new List<SelectListItem>();
                for (int i = 0; i < authors.Count; i++)
                {
                    authorsList.Add(new SelectListItem { Text = authors[i].ToString(), Value = authors[i].ToString() });
                }
                this.Authors = authorsList;

                return Page();
            }
            catch (Exception e)
            {
                return PageWithError(e.Message);
            }
        }

        public async Task<IActionResult> OnPostFilterAuthorAsync(int pageNumber = 1)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {

                FilterDto filters = new FilterDto
                {
                    Author = Filter.Author,
                };

                var books = await _bookService.GetBooksByAuthorPagedAsync(pageNumber, pageSize: 4, filters);

                SetPageItems(_mapper.Map<Paged<PartialBookViewModel>>(books), pageNumber);

                List<string> authors = await _bookService.GetAuthor();
                List<SelectListItem> authorsList = new List<SelectListItem>();
                for (int i = 0; i < authors.Count; i++)
                {
                    authorsList.Add(new SelectListItem { Text = authors[i].ToString(), Value = authors[i].ToString() });
                }
                this.Authors = authorsList;

                return Page();
            }
            catch (Exception e)
            {
                return PageWithError(e.Message);
            }
        }

        public async Task<IActionResult> OnPostFilterPriceAsync(int pageNumber = 1)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                FilterDto filters = new FilterDto
                {
                    MinPrice = Filter.MinPrice,
                    MaxPrice = Filter.MaxPrice
                };

                var books = await _bookService.GetBooksByPricePagedAsync(pageNumber, pageSize: 10, filters);

                SetPageItems(_mapper.Map<Paged<PartialBookViewModel>>(books), pageNumber);

                List<string> authors = await _bookService.GetAuthor();
                List<SelectListItem> authorsList = new List<SelectListItem>();
                for (int i = 0; i < authors.Count; i++)
                {
                    authorsList.Add(new SelectListItem { Text = authors[i].ToString(), Value = authors[i].ToString() });
                }
                this.Authors = authorsList;

                return Page();
            }
            catch (Exception e)
            {
                return PageWithError(e.Message);
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _bookService.DeleteBookAsync(id);
            return await OnGetAsync();
        }
    }
}
