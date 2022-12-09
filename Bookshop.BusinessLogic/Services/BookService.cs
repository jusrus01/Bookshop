using Bookshop.BusinessLogic.Extensions;
using Bookshop.Contracts;
using Bookshop.Contracts.DataTransferObjects.Books;
using Bookshop.Contracts.Generics;
using Bookshop.Contracts.Services;
using Bookshop.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Book = Bookshop.DataLayer.Models.Book;

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
                    Id = book.Id,
                    ISBN = book.ISBN,
                    Title = book.Title,
                    Author = book.Author,
                    Price = book.Price,
                    Genre = _supplierDbSet.Where(b => b.Id == book.GenreId).First().Name,
                    Discount = book.Discount,
                    PriceWithDiscount = Math.Round(book.Price * (1 - book.Discount), 2)
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

            var genre = await _genreDbSet.Where(b => b.Id == book.GenreId).FirstOrDefaultAsync();

            var price = Math.Round(book.Price * (1 - book.Discount), 2);

            return new BookDto
            {
                Id = book.Id,
                ISBN = book.ISBN,
                Title = book.Title,
                Author = book.Author,
                Supplier = _supplierDbSet.Where(b => b.Id == book.SupplierId).First().Name,
                Year = book.Year,
                Pages = book.Pages,
                Description = book.Description,
                Price = book.Price,
                AddedDate = DateTime.Now,
                PriceWithDiscount = price,
                Genre = _genreDbSet.Where(b => b.Id == book.GenreId).First().Name,
            };
        }

        public async Task UpdateAsync(BookDto bookDto)
        {
            await UpdateBookAsync(bookDto);
        }

        private async Task<Book> UpdateBookAsync(BookDto bookDto)
        {
            var newBook = new Book
            {
                Id = bookDto.Id,
                ISBN = bookDto.ISBN,
                Title = bookDto.Title,
                Author = bookDto.Author,
                Year = bookDto.Year,
                Pages = bookDto.Pages,
                Description = bookDto.Description,
                Price = bookDto.Price,
                Discount = bookDto.Discount,
                GenreId = _genreDbSet.Where(b => b.Name == bookDto.Genre).First().Id,
                SupplierId = _supplierDbSet.Where(b => b.Name == bookDto.Supplier).First().Id,
                Created = DateTime.Now
            };
            _bookDbSet.Update(newBook);
            await _uow.SaveChangesAsync();
            return newBook;
        }

        public async Task<Book> SetBookValue(BookDto book)
        {
            return new Book
            {
                Id = book.Id,
                ISBN = book.ISBN,
                Title = book.Title,
                Author = book.Author,
                Year = book.Year,
                Pages = book.Pages,
                Description = book.Description,
                Price = book.Price,
                Discount = book.Discount,
                GenreId = _genreDbSet.Where(b => b.Name == book.Genre).First().Id,
                SupplierId = _supplierDbSet.Where(b => b.Name == book.Supplier).First().Id,
                Created = DateTime.Now
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
                Supplier = _supplierDbSet.Where(b => b.Id == book.SupplierId).First().Name,
                Year = book.Year,
                Pages = book.Pages,
                Description = book.Description,
                Price = book.Price,
                AddedDate = DateTime.Now,
                Discount = book.Discount,
                Genre = _genreDbSet.Where(b => b.Id == book.GenreId).First().Name,
            };
        }

        public async Task<List<GenreDto>> GetGenres()
        {
            List<Genre> dbGenre = _genreDbSet.ToList();

            List<GenreDto> genres = new List<GenreDto>();

            for (int i = 0; i < dbGenre.Count; i++)
            {
                genres.Add(new GenreDto
                {
                    Id = dbGenre[i].Id,
                    Name = dbGenre[i].Name
                });
            }

            return genres;
        }

        public async Task<List<SupplierDto>> GetSupplier()
        {
            List<Supplier> dbSuppliers = _supplierDbSet.ToList();

            List<SupplierDto> suppliers = new List<SupplierDto>();

            for (int i = 0; i < dbSuppliers.Count; i++)
            {
                suppliers.Add(new SupplierDto
                {
                    Id = dbSuppliers[i].Id,
                    Name = dbSuppliers[i].Name
                });
            }

            return suppliers;
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

        public async Task<SupplierDto> GetAuthorAsync(int authorId)
        {
            var author = await _supplierDbSet.SingleOrDefaultAsync(b => b.Id == authorId);

            if (author == null)
            {
                throw new Exception("Genre not found");
            }

            return new SupplierDto
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

        public async Task AddAsync(BookDto bookDto)
        {
            await CreateNewBookAsync(bookDto);
        }

        private async Task<Book> CreateNewBookAsync(BookDto bookDto)
        {
            var newBook = new Book
            {
                ISBN = bookDto.ISBN,
                Title = bookDto.Title,
                Author = bookDto.Author,
                Year = bookDto.Year,
                Pages = bookDto.Pages,
                Description = bookDto.Description,
                Price = bookDto.Price,
                Discount = bookDto.Discount,
                GenreId = int.Parse(bookDto.Genre),
                SupplierId = _supplierDbSet.Where(b => b.Name == bookDto.Supplier).First().Id,
                Genre = _genreDbSet.Where(b => b.Id == int.Parse(bookDto.Genre)).First(),
                Supplier = _supplierDbSet.Where(b => b.Name == bookDto.Supplier).FirstOrDefault(),
                Created = DateTime.Now
            };
            _bookDbSet.Add(newBook);
            await _uow.SaveChangesAsync();
            return newBook;
        }
    }
}
