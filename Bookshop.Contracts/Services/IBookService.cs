using Bookshop.Contracts.DataTransferObjects.Books;
using Bookshop.Contracts.Generics;

namespace Bookshop.Contracts.Services
{
    public interface IBookService
    {
        Task<BookDto> GetBookAsync(int bookId);
        Task<Paged<PartialBookDto>> GetBooksPagedAsync(int page, int pageSize);
        Task DeleteBookAsync(int? id);
        Task UpdateAsync(BookDto bookDto);
        Task<List<GenreDto>> GetGenres();
        Task<List<SupplierDto>> GetSupplier();
        Task AddAsync(BookDto bookDto);
        Task<List<BookCommentDto>> GetComments(int bookId);
        Task AddComment(BookCommentDto comment);
        Task<List<string>> GetAuthor();
        Task<Paged<PartialBookDto>> GetBooksByPricePagedAsync(int page, int pageSize, FilterDto filterDto);
        Task<Paged<PartialBookDto>> GetBooksByTitlePagedAsync(int page, int pageSize, FilterDto filterDto);
        Task<Paged<PartialBookDto>> GetBooksByDiscountPagedAsync(int page, int pageSize, FilterDto filterDto);
        Task<Paged<PartialBookDto>> GetBooksByAuthorPagedAsync(int page, int pageSize, FilterDto filterDto);


    }
}
