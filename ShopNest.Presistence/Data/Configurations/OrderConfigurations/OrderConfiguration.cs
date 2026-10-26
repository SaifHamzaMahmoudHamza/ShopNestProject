using ShopNest.Domain.Entities.OrderModule;

namespace ShopNest.Presistence.Data.Configurations.OrderConfigurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {

            builder.Property(p => p.SubTotal).HasColumnType("decimal(8,2)");
            //builder.Property(p=>p.OrderAddress.Country).HasMaxLength(50);
            //builder.Property(p=>p.OrderAddress.City).HasMaxLength(50);
            //builder.Property(p=>p.OrderAddress.Street).HasMaxLength(50);
            //builder.Property(p=>p.OrderAddress.FirstName).HasMaxLength(50);
            //builder.Property(p=>p.OrderAddress.LastName).HasMaxLength(50);

            builder.OwnsOne(p => p.OrderAddress, OE =>
            {
                OE.Property(p => p.Country).HasMaxLength(50);
                OE.Property(p => p.City).HasMaxLength(50);
                OE.Property(p => p.Street).HasMaxLength(50);
                OE.Property(p => p.FirstName).HasMaxLength(50);
                OE.Property(p => p.LastName).HasMaxLength(50);
            });


        }
    }
}
