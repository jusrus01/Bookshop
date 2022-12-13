using Bookshop.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookshop.DataLayer.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasMany(model => model.Books)
                .WithOne(model => model.Order)
                .HasForeignKey(model => model.OrderId)
                .HasPrincipalKey(model => model.Id)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);
        }
    }
}
