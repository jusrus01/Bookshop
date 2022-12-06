using Bookshop.BusinessLogic.Extensions;
using Bookshop.Contracts;
using Bookshop.Contracts.DataTransferObjects.Books;
using Bookshop.Contracts.Generics;
using Bookshop.Contracts.Services;
using Bookshop.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookshop.BusinessLogic.Services
{
    public class BookService : IBookService
    {
        private readonly DbSet<Book> _bookDbSet;
        private readonly DbSet<Genre> _genreDbSet;
        private readonly DbSet<Supplier> _supplierDbSet;
        private readonly IUnitOfWork _uow;

        public BookService(IUnitOfWork uow)
        {
            _bookDbSet = uow.GetDbSet<Book>();
            _genreDbSet = uow.GetDbSet<Genre>();
            _supplierDbSet = uow.GetDbSet<Supplier>();
            _uow = uow;
        }

        public async Task<Paged<PartialBookDto>> GetBooksPagedAsync(int page, int pageSize)
        {
            return await _bookDbSet.OrderByDescending(book => book.Id)
                .ToPagedAsync(book => new PartialBookDto
                {
                    ISBN = book.ISBN,
                    Title = book.Title,
                    Author = book.Author,
                    Price = book.Price,
                    Genre = book.Genre.Name,
                    Discount = book.Discount
                },
                page,
                pageSize);
        }

        public async Task<BookDto> GetBookAsync(int bookId)
        {
            var book = await _bookDbSet.Where(b => b.Id == bookId).FirstOrDefaultAsync();


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
                Genre = book.Genre?.Name,
            };
        }

        public async Task<BookDto> GetBookISBNAsync(string isbn)
        {
            var book = await _bookDbSet.Where(b => b.ISBN == isbn).FirstOrDefaultAsync();


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
                Genre = book.Genre?.Name,
            };
        }

        public async Task<GenreDto> GetGenreAsync(int genreId)
        {
            var genre = await _genreDbSet.SingleOrDefaultAsync(b => b.Id == genreId);


            if (genre == null)
            {
                throw new Exception("Genre not found");
            }

            return new GenreDto
            {
                Id = genre.Id,
                Name = genre.Name
            };
        }

        public async Task<AuthorDto> GetAuthorAsync(int authorId)
        {
            var author = await _supplierDbSet.SingleOrDefaultAsync(b => b.Id == authorId);

            if (author == null)
            {
                throw new Exception("Genre not found");
            }

            return new AuthorDto
            {
                Id = author.Id,
                Name = author.Name
            };
        }

        public async Task DeleteBookAsync(int? id)
        {
            var book = await _bookDbSet.SingleOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                throw new Exception("No book founded");
            }

            _bookDbSet.Remove(book);
            await _uow.SaveChangesAsync();
        }
    }
}
