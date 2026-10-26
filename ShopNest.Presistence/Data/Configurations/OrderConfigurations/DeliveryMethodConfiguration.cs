using ShopNest.Domain.Entities.OrderModule;

namespace ShopNest.Presistence.Data.Configurations.OrderConfigurations
{
    public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(p => p.Price)
                  .HasColumnType("decimal(8,2)");
            builder.Property(p => p.ShortName)
                  .HasMaxLength(50);
            builder.Property(p => p.Description)
                  .HasMaxLength(50);
            builder.Property(p => p.DeliveryTime)
                  .HasMaxLength(50);
        }
    }
}
