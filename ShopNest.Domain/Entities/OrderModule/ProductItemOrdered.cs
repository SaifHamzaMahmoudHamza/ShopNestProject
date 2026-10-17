namespace ShopNest.Domain.Entities.OrderModule
{
    public class ProductItemOrdered
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string PictureUrl { get; set; } = default!;


    }
}