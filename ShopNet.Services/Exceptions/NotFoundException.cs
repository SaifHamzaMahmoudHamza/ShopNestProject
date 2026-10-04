namespace ShopNet.Services.Exceptions
{
    public abstract class NotFoundException(string message) : Exception(message)
    {
    }
    //Not to be inherited 
    public sealed class ProductNotFoundException(int id) : NotFoundException($"Product with Id {id} is Not Found ")
    { }
    public sealed class BasketNotFoundException(string basketId) : NotFoundException($"Basket with Id {basketId} is Not Found")
    { }
}
