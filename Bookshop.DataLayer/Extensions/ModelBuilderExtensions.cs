using Bookshop.DataLayer.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Bookshop.DataLayer.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ConfigureContracts(this ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                Configure<ICreationTimestamp>(builder, entityType, ConfigureCreationTimestamp);
            }
        }

        private static void Configure<T>(ModelBuilder builder, IMutableEntityType mutableEntityType, Action<ModelBuilder, Type> configuration)
        {
            if (typeof(T).IsAssignableFrom(mutableEntityType.ClrType))
            {
                configuration(builder, mutableEntityType.ClrType);
            }
        }
        
        private static void ConfigureCreationTimestamp(ModelBuilder builder, Type clrType)
        {
            var entity = builder.Entity(clrType);
            entity.Property(nameof(ICreationTimestamp.Created))
               .HasDefaultValue(DateTime.UtcNow);
        }
    }
}
