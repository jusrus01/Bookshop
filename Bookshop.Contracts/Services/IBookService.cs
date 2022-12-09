using Bookshop.Contracts.DataTransferObjects.Books;
using Bookshop.Contracts.Generics;

namespace Bookshop.Contracts.Services
{
    public interface IBookService
    {
        Task<BookDto> GetBookAsync(int bookId);
        Task<Paged<PartialBookDto>> GetBooksPagedAsync(int page, int pageSize);
        Task<GenreDto> GetGenreAsync(int genreId);
        Task<SupplierDto> GetAuthorAsync(int authorId);
        Task DeleteBookAsync(int? id);
        Task<BookDto> GetBookISBNAsync(string isbn);
        Task UpdateAsync(BookDto bookDto);
        Task<List<GenreDto>> GetGenres();
        Task<List<SupplierDto>> GetSupplier();
        Task AddAsync(BookDto bookDto);
    }
}
