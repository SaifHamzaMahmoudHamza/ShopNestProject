using ShopNest.Domain.Entities.OrderModule;

namespace ShopNest.Presistence.Data.Configurations.OrderConfigurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(p => p.Price).HasColumnType("decimal(8,2)");
            builder.OwnsOne(p => p.Product, OE =>
            {
                OE.Property(p => p.Name).HasMaxLength(100);
                OE.Property(p => p.PictureUrl).HasMaxLength(200);
            });

        }
    }
}
