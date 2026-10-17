namespace ShopNest.Domain.Entities.OrderModule
{
    public class OrderItem : BaseEntity<int>
    {
        public ProductItemOrdered Product { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
