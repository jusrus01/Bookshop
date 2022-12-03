using Bookshop.Contracts.DataTransferObjects.Books;
using Bookshop.Contracts.Generics;

namespace Bookshop.Contracts.Services
{
    public interface IBookService
    {
        Task<BookDto> GetBookAsync(int bookId);
        Task<Paged<PartialBookDto>> GetBooksPagedAsync(int page, int pageSize);
        Task<GenreDto> GetGenreAsync(int genreId);
        Task<AuthorDto> GetAuthorAsync(int authorId);
    }
}
