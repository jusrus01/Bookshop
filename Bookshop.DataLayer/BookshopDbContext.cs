using Bookshop.DataLayer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bookshop.DataLayer
{
    public class BookshopDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public BookshopDbContext(DbContextOptions<BookshopDbContext> options)
            :
            base(options)
        {
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}
