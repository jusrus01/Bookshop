using Bookshop.Contracts.DataTransferObjects.Books;
using Bookshop.Contracts.DataTransferObjects.Orders;
using Bookshop.Contracts.Generics;

namespace Bookshop.Contracts.Services
{
    public interface IOrderService
    {
        Task<Paged<PartialOrderDto>> GetBooksPagedAsync(int page, int pageSize);

        Task DeleteOrderAsync(int? id);

        Task<List<BookDto>> GetBooks();

        Task<OrderDto2> GetOrderAsync(int orderId);

        Task UpdateAsync(OrderDto orderDto);

        Task<OrderDto> GetOrderById(int orderId);

        public List<OrderBookDto> GetAllBooks();

        Task<IEnumerable<string>> GetBooksForAutocomplete(string term);

        Task<OrderBookDto2> GetBookByNameAsync(string name);

        Task AddAsync(CreateOrderDto createDto);

        Task<byte[]> GenerateReportAsync(int orderId);
    }
}
