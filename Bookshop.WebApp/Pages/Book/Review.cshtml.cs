using Bookshop.Contracts.Constants;
using Bookshop.Contracts.DataTransferObjects.Books;
using Bookshop.Contracts.Services;
using Bookshop.WebApp.Attributes;
using Bookshop.WebApp.PageModels;
using Bookshop.WebApp.ViewModels.Books;
using Microsoft.AspNetCore.Mvc;

namespace Bookshop.WebApp.Pages.Book
{
    [AuthorizeAnyOfTheRoles(BookshopRoles.Administrator, BookshopRoles.Manager)]
    public class ReviewModel : BookshopPageModel
    {
        private readonly IBookService _bookService;
        public List<BookCommentDto> Comments { get; set; }
        public BookDto Book { get; set; }

        [BindProperty]
        public CommentViewModel CommentInput { get; set; } = new();

        public ReviewModel(IBookService bookService) : base(null)
        {
            _bookService = bookService;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            Book = await _bookService.GetBookAsync(id);
            Comments = await _bookService.GetComments(id);

            if (Book == null)
            {
                return RedirectToPage("/notFound");
            }

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
                BookCommentDto comment = new BookCommentDto { BookId = CommentInput.BookId, Comment = CommentInput.Comment, Score = CommentInput.Score, UserId = CommentInput.ClientName };

                await _bookService.AddComment(comment);
            }
            catch (Exception e)
            {
                return PageWithError(e.Message);
            }

            return await OnGet(CommentInput.BookId);
        }
    }
}
