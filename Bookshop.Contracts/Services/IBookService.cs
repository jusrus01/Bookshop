using Bookshop.Contracts.DataTransferObjects.Books;

namespace Bookshop.Contracts.Services
{
    public interface IBookService
    {
        Task<BookDto> GetBooktAsync(int bookId);
    }
}
