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
        Task AddAsync(OrderDto orderDto);

        Task<OrderDto> GetOrderAsync(int orderId);
        Task UpdateAsync(OrderDto orderDto);
    }
}
