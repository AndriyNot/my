using Catalog.Host.Data.Entities;

namespace Catalog.Host.Data.EntityConfigurations
{
    public class OrderDetailEntityTypeConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(od => od.Id);

            builder.Property(od => od.UserId)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(od => od.Amount)
                .IsRequired();

            builder.HasOne(od => od.CatalogItem)
                .WithMany()
                .HasForeignKey(od => od.CatalogItemId);
        }
    }
}
