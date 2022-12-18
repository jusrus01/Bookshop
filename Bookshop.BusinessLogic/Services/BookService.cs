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
        private readonly DbSet<Rating> _ratingDbSet;
        private readonly DbSet<ApplicationUser> _usersDbSet;
        private readonly IUnitOfWork _uow;

        public BookService(IUnitOfWork uow)
        {
            _bookDbSet = uow.GetDbSet<Book>();
            _genreDbSet = uow.GetDbSet<Genre>();
            _supplierDbSet = uow.GetDbSet<Supplier>();
            _ratingDbSet = uow.GetDbSet<Rating>();
            _usersDbSet = uow.GetDbSet<ApplicationUser>();
            _uow = uow;
        }

        public async Task<Paged<PartialBookDto>> GetBooksPagedAsync(int page, int pageSize)
        {
            return await _bookDbSet.Where(x => x.OrderId == null).OrderByDescending(book => book.Id)
                .ToPagedAsync(book => new PartialBookDto
                {
                    Id = book.Id,
                    ISBN = book.ISBN,
                    Title = book.Title,
                    Author = book.Author,
                    Price = book.Price,
                    Genre = _genreDbSet.Where(b => b.Id == book.GenreId).First().Name,
                    Discount = book.Discount,
                    PriceWithDiscount = Math.Round(book.Price * (1 - book.Discount), 2)
                },
                page,
                pageSize);
        }

        public async Task<Paged<PartialBookDto>> GetBooksByPricePagedAsync(int page, int pageSize, FilterDto filterDto)
        {
            return await _bookDbSet.Where(x => x.OrderId == null).Where(x => x.Price >= filterDto.MinPrice && x.Price <= filterDto.MaxPrice).OrderByDescending(book => book.Id)
            .ToPagedAsync(book => new PartialBookDto
            {
                Id = book.Id,
                ISBN = book.ISBN,
                Title = book.Title,
                Author = book.Author,
                Price = book.Price,
                Genre = _genreDbSet.Where(b => b.Id == book.GenreId).First().Name,
                Discount = book.Discount,
                PriceWithDiscount = Math.Round(book.Price * (1 - book.Discount), 2)
            },
            page,
            pageSize);

        }

        public async Task<Paged<PartialBookDto>> GetBooksByDiscountPagedAsync(int page, int pageSize, FilterDto filterDto)
        {
            float min = (float)filterDto.MinDicount / 100;
            float max = (float)filterDto.MaxDicount / 100;
            return await _bookDbSet.Where(book => book.OrderId == null).Where(x => x.Discount >= min && x.Discount <= max).OrderByDescending(book => book.Id)
            .ToPagedAsync(book => new PartialBookDto
            {
                Id = book.Id,
                ISBN = book.ISBN,
                Title = book.Title,
                Author = book.Author,
                Price = book.Price,
                Genre = _genreDbSet.Where(b => b.Id == book.GenreId).First().Name,
                Discount = book.Discount,
                PriceWithDiscount = Math.Round(book.Price * (1 - book.Discount), 2)
            },
            page,
            pageSize);

        }

        public async Task<Paged<PartialBookDto>> GetBooksByTitlePagedAsync(int page, int pageSize, FilterDto filterDto)
        {
            return await _bookDbSet.Where(book => book.OrderId == null).Where(x => x.OrderId != null).Where(x => x.Title.Contains(filterDto.Title)).OrderByDescending(book => book.Id)
            .ToPagedAsync(book => new PartialBookDto
            {
                Id = book.Id,
                ISBN = book.ISBN,
                Title = book.Title,
                Author = book.Author,
                Price = book.Price,
                Genre = _genreDbSet.Where(b => b.Id == book.GenreId).First().Name,
                Discount = book.Discount,
                PriceWithDiscount = Math.Round(book.Price * (1 - book.Discount), 2)
            },
            page,
            pageSize);

        }

        public async Task<Paged<PartialBookDto>> GetBooksByAuthorPagedAsync(int page, int pageSize, FilterDto filterDto)
        {
            return await _bookDbSet.Where(book => book.OrderId == null).Where(x => x.Author.Contains(filterDto.Author)).OrderByDescending(book => book.Id)
        .ToPagedAsync(book => new PartialBookDto
        {
            Id = book.Id,
            ISBN = book.ISBN,
            Title = book.Title,
            Author = book.Author,
            Price = book.Price,
            Genre = _genreDbSet.Where(b => b.Id == book.GenreId).First().Name,
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
                GenreId = int.Parse(bookDto.Genre),
                SupplierId = int.Parse(bookDto.Supplier),
                Genre = _genreDbSet.Where(b => b.Id == int.Parse(bookDto.Genre)).First(),
                Supplier = _supplierDbSet.Where(b => b.Id == int.Parse(bookDto.Supplier)).FirstOrDefault(),
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

        public async Task<List<string>> GetAuthor()
        {
            List<string> authors = _bookDbSet.Where(book => book.OrderId == null).Select(x => x.Author).ToList();
            authors = authors.DistinctBy(a => a.First()).ToList();
            return authors;
        }

        public async Task<List<BookCommentDto>> GetComments(int bookId)
        {
            List<Rating> dbRates = _ratingDbSet.Where(x => x.BookId == bookId).ToList();

            List<BookCommentDto> comments = new List<BookCommentDto>();

            for (int i = 0; i < dbRates.Count; i++)
            {
                comments.Add(new BookCommentDto
                {
                    Score = dbRates[i].Score,
                    Comment = dbRates[i].Comment,
                    UserId = _usersDbSet.Where(x => x.Id == dbRates[i].UserId).FirstOrDefault().FirstName + " " + _usersDbSet.Where(x => x.Id == dbRates[i].UserId).FirstOrDefault().LastName
                });
            }

            return comments;
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
                SupplierId = int.Parse(bookDto.Supplier),
                Created = DateTime.Now
            };
            _bookDbSet.Add(newBook);
            await _uow.SaveChangesAsync();
            return newBook;
        }


        public async Task AddComment(BookCommentDto comment)
        {
            await CreateNewComment(comment);
        }

        private async Task<Rating> CreateNewComment(BookCommentDto comment)
        {
            var newRating = new Rating
            {
                Created = DateTime.Now,
                User = _usersDbSet.Where(x => x.Id == comment.UserId).FirstOrDefault(),
                UserId = comment.UserId,
                BookId = comment.BookId,
                Book = _bookDbSet.Where(x => x.Id == comment.BookId).FirstOrDefault(),
                Score = comment.Score,
                Comment = comment.Comment
            };
            _ratingDbSet.Add(newRating);
            await _uow.SaveChangesAsync();
            return newRating;
        }
    }
}
