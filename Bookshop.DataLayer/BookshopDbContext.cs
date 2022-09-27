using Bookshop.DataLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bookshop.DataLayer
{
    public class BookshopDbContext : IdentityDbContext<IdentityUser>
    {
        public BookshopDbContext(DbContextOptions<BookshopDbContext> options)
            :
            base(options)
        {
        }

        // This step is necessary
        public DbSet<Demo> Demos { get; set; } // Table will be name Demos
                                               // After adding this you should run these commands
                                               // Add-Migration MigrationNameShouldBeHere -Project Bookshop.DataLayer -StartUpProject Bookshop.WebApp
                                               // Update-Database -Project Bookshop.DataLayer -StartUpProject Bookshop.WebApp

        // Note: most likely it will crash at first, because we would need to configure some stuff.
        // e.g. add foreign keys, primary keys and so on.
        // We can do that with data annotations like: [Key] (see in Demo.cs)
        // However, I would encourage to use Fluent API since it gives us more control.
        // Look below for example :)

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configuring demo model
            // Note: we can extract these configuration to another file but I am too lazy to do it now :/
            builder.Entity<Demo>()
                .HasKey(model => model.Id);

            builder.Entity<Demo>()
                .HasOne(model => model.User)
                .WithMany()
                .HasForeignKey(model => model.UserId);
        }
    }
}
