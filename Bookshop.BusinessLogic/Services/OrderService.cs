using AutoMapper;
using Bookshop.BusinessLogic.Extensions;
using Bookshop.Contracts;
using Bookshop.Contracts.DataTransferObjects.Books;
using Bookshop.Contracts.DataTransferObjects.Orders;
using Bookshop.Contracts.Enums;
using Bookshop.Contracts.Generics;
using Bookshop.Contracts.Services;
using Bookshop.DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Book = Bookshop.DataLayer.Models.Book;

namespace Bookshop.BusinessLogic.Services
{
    public class OrderService : IOrderService
    {
        private readonly DbSet<Order> _orderDBSet;
        private readonly DbSet<OrderState> _orderStateDBSet;
        private readonly DbSet<Book> _bookDbSet;
        private readonly DbSet<Supplier> _supplierDbSet;
        private readonly DbSet<Genre> _genreDbSet;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderService(
            IUnitOfWork uow,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _orderDBSet = uow.GetDbSet<Order>();
            _orderStateDBSet = uow.GetDbSet<OrderState>();
            _bookDbSet = uow.GetDbSet<Book>();
            _supplierDbSet = uow.GetDbSet<Supplier>();
            _genreDbSet = uow.GetDbSet<Genre>();
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _uow = uow;
        }


        public async Task<IEnumerable<string>> GetBooksForAutocomplete(string term) =>
            await _bookDbSet.Where(book => book.OrderId == null && book.Title.Contains(term))
                .Select(book => book.Title)
                .Distinct()
                .Take(10)
                .ToListAsync();

        public async Task<CreateOrderBookDto> GetBookByNameAsync(string name)
        {
            var book = await _bookDbSet.FirstOrDefaultAsync(book => 
                book.OrderId == null &&
                book.Title == name);

            if (book == null)
            {
                return null;
            }

            return new CreateOrderBookDto
            {
                Id = book.Id,
                Name = book.Title,
                Price = book.Price - book.Price * book.Discount // Assuming that input is always [0, 1]
            };
        }

        public async Task<Paged<PartialOrderDto>> GetBooksPagedAsync(int page, int pageSize)
        {
            var user = _httpContextAccessor.HttpContext.User;
            var userId = user.GetAuthenticatedUserId();
            var canManage = user.IsAdminOrManager();

            return await _orderDBSet
                .Include(order => order.User)
                .Where(order => canManage || order.UserId == userId)
                .OrderByDescending(order => order.Created)
                .ToPagedAsync(order => new PartialOrderDto
                {
                    Id = order.Id,
                    ClientName = $"{order.User.FirstName} {order.User.LastName}",
                    Created = order.Created,
                    Sum = order.Sum,
                    PostalCode = order.PostalCode
                },
                page,
                pageSize) ;
        }

        public async Task DeleteOrderAsync(int? id)
        {
            var order = await _orderDBSet.SingleOrDefaultAsync(b => b.Id == id);

            if (order == null)
            {
                throw new Exception("No order founded");
            }

            _orderDBSet.Remove(order);
            await _uow.SaveChangesAsync();
        }

        public async Task AddAsync(CreateOrderDto createDto)
        {
            using var transaction = await _uow.GetDatabase().BeginTransactionAsync();
            
            try
            {
                var timestamp = DateTime.Now;
                var state = new OrderState
                {
                    Created = timestamp,
                    Status = OrderStatus.NotPayed
                };

                _orderStateDBSet.Add(state);
                await _uow.SaveChangesAsync();

                var order = new Order
                {
                    Created = timestamp,
                    Sum = createDto.SelectedBooks.Sum(book => book.Price),
                    PostalCode = createDto.PostalCode,
                    Address = createDto.Address,
                    ClientComment = createDto.ClientCommentForCourier,
                    OrderMethod = createDto.DeliveryMethod,
                    PaymentMethod = createDto.PaymentMethod,
                    ExpectedDelivery = timestamp.AddDays(14),
                    PaymentDate = timestamp.AddDays(3),
                    CourierComment = "Waiting for payment",
                    UserId = _httpContextAccessor.HttpContext.User.GetAuthenticatedUserId(),
                    StatusId = state.Id,
                };
                _orderDBSet.Add(order);
                await _uow.SaveChangesAsync();

                var bookIds = createDto.SelectedBooks.Select(book => book.Id).ToList();
                var books = await _bookDbSet.Where(book => bookIds.Contains(book.Id)).ToListAsync();

                foreach (var book in books)
                {
                    book.OrderId = order.Id;
                }
                _bookDbSet.UpdateRange(books);
                await _uow.SaveChangesAsync();

                transaction.Commit();
            }
            catch
            {
                throw new Exception("Failed to create order");
            }
        }

        public async Task AddDeprecatedAsync(OrderDto orderDto)
        {
            await CreateNewOrderAsync(orderDto);
        }

        private async Task<OrderState> CreateNewOrderStatusAsync(OrderStatus orderStatus, string comment, DateTime created)
        {
            var newOrderState = new OrderState
            {
                Status = orderStatus,
                Comment = comment,
                Created = created
            };

            _orderStateDBSet.Add(newOrderState);
            await _uow.SaveChangesAsync();
            return newOrderState;
        }

            private async Task<Order> CreateNewOrderAsync(OrderDto orderDto)
        {
            var state = await CreateNewOrderStatusAsync(orderDto.Status, "Labas", DateTime.UtcNow);

            var newOrder = new Order
            {
                Id = orderDto.Id,              
                Created = DateTime.UtcNow,
                Sum = orderDto.Sum,
                PostalCode = orderDto.PostalCode,
                Address = orderDto.Address,
                ClientComment = orderDto.ClientComment,
                OrderMethod = orderDto.OrderMethod,
                PaymentMethod = orderDto.PaymentMethod,
                ExpectedDelivery = DateTime.UtcNow.AddDays(14),
                PaymentDate = DateTime.UtcNow,
                CourierComment = "Started",
                UserId = orderDto.UserId,
                StatusId = state.Id,
            };
            _orderDBSet.Add(newOrder);
            await _uow.SaveChangesAsync();
            var book = await _bookDbSet.FirstAsync(book=> book.Id == orderDto.BookId);
            book.OrderId = newOrder.Id;
            _bookDbSet.Update(book);
            await _uow.SaveChangesAsync();
            return newOrder;
        }

        public async Task<List<BookDto>> GetBooks()
        {
            List<Book> dbBook = _bookDbSet.ToList();

            List<BookDto> books = new List<BookDto>();

            for (int i = 0; i < dbBook.Count; i++)
            {
                books.Add(new BookDto
                {
                    Id = dbBook[i].Id,
                    ISBN = dbBook[i].ISBN,
                    Title = dbBook[i].Title,
                    Author = dbBook[i].Author,
                    Year = dbBook[i].Year,
                    Pages = dbBook[i].Pages,
                    Description = dbBook[i].Description,
                    Price = dbBook[i].Price,
                    AddedDate = DateTime.UtcNow,
                    PriceWithDiscount = dbBook[i].Price * (dbBook[i].Discount/100),
                    Discount = dbBook[i].Discount,
                    Supplier = _supplierDbSet.Where(b => b.Id == dbBook[i].SupplierId).First().Name,
                    Genre = _genreDbSet.Where(b => b.Id == dbBook[i].GenreId).First().Name
                });
            }

            return books;
        }

        public async Task<OrderDto> GetOrderAsync(int orderId)
        {
            
            var order = await _orderDBSet.Include(book=> book.Status).Include(book => book.Books).Where(b => b.Id == orderId).FirstOrDefaultAsync();


            if (order == null)
            {
                throw new Exception("Order not found");
            }

            return new OrderDto
            {
                Id = order.Id,
                
                Sum = order.Sum,
                PostalCode = order.PostalCode,
                Address = order.Address,
                ClientComment = order.ClientComment,
                OrderMethod = order.OrderMethod,
                PaymentMethod = order.PaymentMethod,
                ExpectedDelivery = order.ExpectedDelivery,
                PaymentDate = order.PaymentDate,
                Status = order.Status.Status,
                UserId = order.UserId,
                Books = order.Books.Select(book => new BookDtoDto { Id = book.Id, Name = book.Title }).ToList()
            };
        }

        public async Task<OrderDto> GetOrderById(int orderId)
        {
            var order = await _orderDBSet.FindAsync(orderId);


            if (order == null)
            {
                throw new Exception("Order not found");
            }

            return _mapper.Map<OrderDto>(order);
        }

        private async Task<Order> UpdateOrderAsync(OrderDto orderDto)
        {
            var state = await CreateNewOrderStatusAsync(orderDto.Status, "Labas", DateTime.UtcNow);

            var newOrder = new Order
            {
                Id = orderDto.Id,
                Created = DateTime.UtcNow,
                Sum = orderDto.Sum,
                PostalCode = orderDto.PostalCode,
                Address = orderDto.Address,
                ClientComment = orderDto.ClientComment,
                OrderMethod = orderDto.OrderMethod,
                PaymentMethod = orderDto.PaymentMethod,
                ExpectedDelivery = DateTime.UtcNow.AddDays(14),
                PaymentDate = DateTime.UtcNow,
                CourierComment = "Started",
                UserId = orderDto.UserId,
                StatusId = state.Id,
            };
            _orderDBSet.Update(newOrder);
            await _uow.SaveChangesAsync();
            var book = await _bookDbSet.FirstAsync(book => book.Id == orderDto.BookId);
            book.OrderId = newOrder.Id;
            _bookDbSet.Update(book);
            await _uow.SaveChangesAsync();
            return newOrder;
        }

        public async Task UpdateAsync(OrderDto orderDto)
        {
            await UpdateOrderAsync(orderDto);
        }

        public List<OrderBookDto> GetAllBooks()
        {
            List<Book> dbBook = _bookDbSet.ToList();

            List<OrderBookDto> books = new List<OrderBookDto>();

            for (int i = 0; i < dbBook.Count; i++)
            {
                books.Add(new OrderBookDto
                {
                    Id = dbBook[i].Id,
                    ISBN = dbBook[i].ISBN,
                    Title = dbBook[i].Title,
                    Author = dbBook[i].Author,
                    Year = dbBook[i].Year,
                    Pages = dbBook[i].Pages,
                    Description = dbBook[i].Description,
                    Price = dbBook[i].Price,
                    Created = DateTime.UtcNow,
                    Discount = dbBook[i].Price * dbBook[i].Discount,
                    OrderId = _orderDBSet.Where(b => b.Id == dbBook[i].SupplierId).First().Id,
                    GenreId = _genreDbSet.Where(b => b.Id == dbBook[i].GenreId).First().Id
                });
            }

            return books;
        }
    }
}
