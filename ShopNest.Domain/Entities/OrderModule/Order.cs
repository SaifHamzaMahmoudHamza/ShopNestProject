namespace ShopNest.Domain.Entities.OrderModule
{
    //COMPOSITE ENTITY consists of multiple Entities
    public class Order : BaseEntity<Guid>
    {
        public string UserEmail { get; set; } = default!;
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public OrderAddress OrderAddress { get; set; } = default!;
        public DeliveryMethod DeliveryMethod { get; set; } = default!;
        public int DeliveryMethodId { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = [];
        // SubTotla + Tax + Delivery + etc
        //Computed funtion 
        public decimal GetTotal() => SubTotal + DeliveryMethod.Price;
        //Quantity * Price
        public decimal SubTotal { get; set; }
    }
}
