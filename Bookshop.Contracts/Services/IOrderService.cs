﻿using Bookshop.Contracts.DataTransferObjects.Books;
using Bookshop.Contracts.DataTransferObjects.Orders;
using Bookshop.Contracts.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop.Contracts.Services
{
    public interface IOrderService
    {
        Task<Paged<PartialOrderDto>> GetBooksPagedAsync(int page, int pageSize);
        Task DeleteOrderAsync(int? id);
    }
}