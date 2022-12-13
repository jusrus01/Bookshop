using Bookshop.BusinessLogic.Extensions;
using Bookshop.Contracts;
using Bookshop.Contracts.Constants;
using Bookshop.Contracts.DataTransferObjects.Books;
using Bookshop.Contracts.DataTransferObjects.Orders;
using Bookshop.Contracts.DataTransferObjects.Users;
using Bookshop.Contracts.Enums;
using Bookshop.Contracts.Generics;
using Bookshop.Contracts.Services;
using Bookshop.DataLayer.Models;
using com.sun.org.apache.xpath.@internal.operations;
using java.awt.print;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public OrderService(IUnitOfWork uow)
        {
            _orderDBSet = uow.GetDbSet<Order>();
            _orderStateDBSet = uow.GetDbSet<OrderState>();
            _bookDbSet = uow.GetDbSet<Book>();
            _supplierDbSet = uow.GetDbSet<Supplier>();
            _genreDbSet = uow.GetDbSet<Genre>();
            _uow = uow;
        }
        public async Task<Paged<PartialOrderDto>> GetBooksPagedAsync(int page, int pageSize)
        {
            return await _orderDBSet.Include(order => order.User).OrderByDescending(order => order.Id)
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

        public async Task AddAsync(OrderDto orderDto)
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
                BookId = orderDto.BookId
            };
            _orderDBSet.Add(newOrder);
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
            
            var order = await _orderDBSet.Where(b => b.Id == orderId).FirstOrDefaultAsync();


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
                BookId = _bookDbSet.Where(b => b.Id == order.Id).First().Title
            };
        }
    }
}
