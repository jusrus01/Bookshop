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
            return MapListToPaged(items, page, pageSize);
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
            return MapListToPaged(items, page, pageSize);
        }

        private static Paged<T> MapListToPaged<T>(IList<T> items, int page, int pageSize) where T : class
        {
            return new Paged<T>
            {
                Items = items.Count > pageSize ? items.SkipLast(1) : items,
                NextPageIsEmpty = items.Count <= pageSize,
                PreviousPageIsEmpty = page <= 1,
                Count = items.Count != 0 ? items.Count - 1 : 0
            };
        }
    }
}
