using Bookshop.BusinessLogic.Extensions;
using Bookshop.Contracts;
using Bookshop.Contracts.DataTransferObjects.Books;
using Bookshop.Contracts.DataTransferObjects.Orders;
using Bookshop.Contracts.Generics;
using Bookshop.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop.BusinessLogic.Services
{
    public class OrderService
    {
        private readonly DbSet<Order> _orderDBSet;
        private readonly IUnitOfWork _uow;

        public OrderService(IUnitOfWork uow)
        {
            _orderDBSet = uow.GetDbSet<Order>();

            _uow = uow;
        }
        public async Task<Paged<PartialOrderDto>> GetBooksPagedAsync(int page, int pageSize)
        {
            return await _orderDBSet.Include(order => order.User).OrderByDescending(order => order.Id)
                .ToPagedAsync(order => new PartialOrderDto
                {
                    ClientName = $"{order.User.FirstName} {order.User.LastName}",
                    Created = order.Created,
                    Sum = order.Sum,
                    PostalCode = order.PostalCode
                },
                page,
                pageSize);
        }
    }
}
