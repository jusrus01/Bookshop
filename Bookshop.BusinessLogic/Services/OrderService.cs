using AutoMapper;
using Bookshop.BusinessLogic.Builders;
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
using System.Text;
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


        public async Task<byte[]> GenerateReportAsync(int orderId)
        {
            var order = await _orderDBSet.Include(order => order.User)
                .Include(order => order.Books)
                .FirstAsync(order => order.Id == orderId);

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<html><body>");

            stringBuilder.AppendLine($"<h1>Order #{orderId}</h1>");
            stringBuilder.AppendLine($"<table>" +
                $"<tr>" +
                $"<td><h4>Order date</h4></td><td><h4>Payment date</h4></td><td><h4>Payment method&nbsp;</h4></td><td><h4>Delivery method</h4></td>" +
                $"</tr>" +
                $"<tr>" +
                $"<td>{order.Created}&nbsp;</td><td>{order.PaymentDate}&nbsp;</td><td>{order.PaymentMethod.GetString()}&nbsp;</td><td>{order.OrderMethod.GetString()}&nbsp;</td>" +
                $"</tr>" +
                $"</table>");

            stringBuilder.AppendLine($"<table>" +
                $"<tr>" +
                $"<td><h4>Customer name&nbsp;&nbsp;</h4></td><td><h4>Address</h4></td><td><h4>Phone</h4></td>" +
                $"</tr>" +
                $"<tr>" +
                $"<td>{order.User.FirstName} {order.User.LastName}&nbsp;</td><td>{order.User.Address}&nbsp;</td><td>{order.User.PhoneNumber}&nbsp;</td>" +
                $"</tr>" +
                $"</table>");

            var bookCount = 1;
            stringBuilder.AppendLine("<h4>Order items</h4>");
            stringBuilder.AppendLine($"<table border='1'><tr>" +
                    $"<td><bold>#</bold></td>" +
                    $"<td><bold>Author</bold></td>" +
                    $"<td><bold>Book title</bold></td>" +
                    $"<td><bold>Pages</bold></td>" +
                    $"<td><bold>Price</bold></td>" +
                    $"</tr>");

            foreach (var book in order.Books)
            {
                stringBuilder.AppendLine($"<tr>" +
                    $"<td>{bookCount}</td>" +
                    $"<td>{book.Author}</td>" +
                    $"<td>{book.Title}</td>" +
                    $"<td>{book.Pages}</td>" +
                    $"<td>{book.Price - book.Price * book.Discount} €</td>" +
                    $"</tr>");
                bookCount++;
            }
            stringBuilder.AppendLine($"</table>");
            stringBuilder.AppendLine($"<h4>Total price: {order.Sum} €</h4>");
            stringBuilder.AppendLine("<body/></html>");
            return PdfBuilder.Build(stringBuilder.ToString());
        }

        public async Task<IEnumerable<string>> GetBooksForAutocomplete(string term) =>
            await _bookDbSet.Where(book => book.OrderId == null && book.Title.Contains(term))
                .Select(book => book.Title)
                .Distinct()
                .Take(10)
                .ToListAsync();

        public async Task<OrderBookDto2> GetBookByNameAsync(string name)
        {
            var book = await _bookDbSet.FirstOrDefaultAsync(book => 
                book.OrderId == null &&
                book.Title == name);

            if (book == null)
            {
                return null;
            }

            return new OrderBookDto2
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
                    PostalCode = order.PostalCode,
                    UserId = order.UserId
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

        public async Task UpdateAsync(EditOrderDto editDto)
        {
            var order = await _orderDBSet.FirstAsync(order => order.Id == editDto.Id);
            order.PostalCode = editDto.PostalCode;
            order.Address = editDto.Address;
            order.CourierComment = editDto.CourierComment;
            order.OrderMethod = editDto.DeliveryMethod;
            order.PaymentMethod = editDto.PaymentMethod;
            _orderDBSet.Update(order);
            if (editDto.Status != OrderStatus.None)
            {
                await _orderStateDBSet.AddAsync(new OrderState
                {
                    Comment = editDto.Comment,
                    Status = editDto.Status,
                    Created = editDto.Created,
                    OrderId = order.Id
                });
            }
            await _uow.SaveChangesAsync();
        }

        public async Task AddAsync(CreateOrderDto createDto)
        {
            using var transaction = await _uow.GetDatabase().BeginTransactionAsync();
            
            try
            {
                var timestamp = DateTime.Now;

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
                    CourierComment = "No comment",
                    UserId = _httpContextAccessor.HttpContext.User.GetAuthenticatedUserId(),
                };
                _orderDBSet.Add(order);
                await _uow.SaveChangesAsync();

                var state = new OrderState
                {
                    Created = timestamp,
                    Status = OrderStatus.NotPayed,
                    OrderId = order.Id,
                    Comment = "Waiting for payment"
                };

                _orderStateDBSet.Add(state);
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

        public async Task<List<BookDto>> GetBooks()
        {
            List<Book> dbBook = await _bookDbSet.ToListAsync();

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

        public async Task<OrderDto2> GetOrderAsync(int orderId)
        {
            var order = await _orderDBSet
                .Include(order => order.Books)
                .SingleAsync(order => order.Id == orderId);
            var states = await _orderStateDBSet.Where(state => state.OrderId == orderId)
                .OrderBy(state => state.Created)
                .Select(state => new OrderStateDto
                {
                    Comment = state.Comment,
                    Status = state.Status,
                    Created = state.Created
                })
                .ToListAsync();

            return new OrderDto2
            {
                Sum = order.Sum,
                PostalCode = order.PostalCode,
                Address = order.Address,
                ClientComment = order.ClientComment,
                OrderMethod = order.OrderMethod,
                PaymentMethod = order.PaymentMethod,
                PaymentDate = order.PaymentDate,
                States = states.ToList(),
                Books = order.Books.Select(book => new OrderBookDto2
                {
                    Name = book.Title,
                    Price = book.Price - book.Price * book.Discount,
                    Author = book.Author
                }).ToList()
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
