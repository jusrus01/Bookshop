using Bookshop.Contracts;
using Bookshop.Contracts.DataTransferObjects.Books;
using Bookshop.Contracts.Services;
using Bookshop.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookshop.BusinessLogic.Services
{
    public class BookService : IBookService
    {
        private readonly DbSet<Book> _bookDbSet;

        public BookService(IUnitOfWork uow)
        {
            _bookDbSet = uow.GetDbSet<Book>();
        }

        public Task<BookDto> GetBooktAsync(int bookId)
        {
            throw new NotImplementedException();
        }

        public async Task<BookDto> GetBookAsync(int bookId)
        {
            var book = await _bookDbSet.SingleOrDefaultAsync(b => b.Id == bookId);

            if (book == null)
            {
                throw new Exception("Book not found");
            }

            return new BookDto
            {
                Id = book.Id,
                ISBN = book.ISBN,
                Title = book.Title,
                Author = book.Author,
                Year = book.Year,
                Pages = book.Pages,
                Description = book.Description,
                Price = book.Price,
                AddedDate = DateTime.Now,
                Discount = book.Discount,
                Genre = book.Genre,
            };
        }
    }
}
