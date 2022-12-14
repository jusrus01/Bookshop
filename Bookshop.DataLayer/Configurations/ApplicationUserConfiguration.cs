using Bookshop.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookshop.DataLayer.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        private const int MaxUserNamesLength = 100;
        
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(model => model.UserName)
                .HasMaxLength(40)
                .IsRequired();

            builder.Property(model => model.FirstName)
                .HasMaxLength(MaxUserNamesLength)
                .IsRequired();

            builder.Property(model => model.LastName)
                .HasMaxLength(MaxUserNamesLength)
                .IsRequired();

            builder.Property(model => model.Email)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(model => model.PhoneNumber)
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(model => model.Address)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(model => model.LastLogin)
                .HasDefaultValue(DateTime.Now);

            builder.HasMany(model => model.Orders)
                .WithOne(model => model.User)
                .HasForeignKey(model => model.UserId)
                .HasPrincipalKey(model => model.Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
