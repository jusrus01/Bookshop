using Microsoft.EntityFrameworkCore;

namespace Bookshop.Contracts
{
    public interface IUnitOfWork
    {
        DbSet<T> GetDbSet<T>() where T : class;

        Task SaveChangesAsync();
    }
}
