using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Bookshop.Contracts
{
    public interface IUnitOfWork
    {
        DbSet<T> GetDbSet<T>() where T : class;

        DatabaseFacade GetDatabase();

        Task SaveChangesAsync();
    }
}
