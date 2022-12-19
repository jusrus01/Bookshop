using Bookshop.DataLayer.Extensions;
using Bookshop.DataLayer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Storage> Storages { get; set; }
        
        public DbSet<City> Cities { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderState> OrderStates { get; set; }


        public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ConfigureContracts();
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
