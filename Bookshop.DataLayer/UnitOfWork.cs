using Bookshop.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Bookshop.DataLayer
{
    /// <summary>
    /// Not a good implementation but should be comfortable to use
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookshopDbContext _context;

        public UnitOfWork(BookshopDbContext context)
        {
            _context = context;
        }

        public DatabaseFacade GetDatabase()
        {
            return _context.Database;
        }

        public DbSet<T> GetDbSet<T>() where T : class
        {
            return _context.Set<T>();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
