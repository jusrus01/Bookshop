using Bookshop.Contracts.Generics;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bookshop.BusinessLogic.Extensions
{
    public static class OrderedQueryableExtensions
    {
        public static async Task<Paged<T>> ToPagedAsync<T>(
            this IOrderedQueryable<T> queryable,
            int page,
            int pageSize) where T : class
        {
            var items = await queryable.Skip((page - 1) * pageSize)
                .Take(pageSize + 1)
                .ToListAsync();

            return new Paged<T>(items, page, pageSize);
        }

        public static async Task<Paged<TDto>> ToPagedAsync<TModel, TDto>(
            this IOrderedQueryable<TModel> queryable,
            Expression<Func<TModel, TDto>> selector,
            int page,
            int pageSize) 
            where TModel : class
            where TDto : class
        {
            var items = await queryable.Skip((page - 1) * pageSize)
                .Take(pageSize + 1)
                .Select(selector)
                .ToListAsync();

            return new Paged<TDto>(items, page, pageSize);
        }
    }
}
