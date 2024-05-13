namespace Shops.Models;

public class ShopExceptions : Exception
{
    public ShopExceptions(string message)
        : base(message) { }
}