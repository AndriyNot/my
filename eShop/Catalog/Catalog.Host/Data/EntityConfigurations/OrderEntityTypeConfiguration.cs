using Catalog.Host.Data.Entities;

namespace Catalog.Host.Data.EntityConfigurations
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.UserId)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(o => o.OrderDate)
                .IsRequired();

            builder.HasMany(o => o.OrderDetails)
                .WithOne()
                .HasForeignKey(od => od.OrderId);
        }
    }
}
