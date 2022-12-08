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
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop.BusinessLogic.Services
{
    public class OrderService : IOrderService
    {
        private readonly DbSet<Order> _orderDBSet;
        private readonly DbSet<OrderState> _orderStateDBSet;
        private readonly IUnitOfWork _uow;

        public OrderService(IUnitOfWork uow)
        {
            _orderDBSet = uow.GetDbSet<Order>();
            _orderStateDBSet = uow.GetDbSet<OrderState>();

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
                StatusId = state.Id
            };
            _orderDBSet.Add(newOrder);
            await _uow.SaveChangesAsync();
            return newOrder;
        }
    }
}
