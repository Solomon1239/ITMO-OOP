using Shops.Models;

namespace Shops.Entities;

public class Product
{
    public Product(string name, decimal price, int inStock)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ShopExceptions("Incorrect product's name");
        if (price < 1) throw new ShopExceptions("Price cannot be 0 or less");
        if (inStock < 1) throw new ShopExceptions("Amount of products cannot be 0 or less");
        Name = name;
        Price = price;
        InStock = inStock;
    }

    public Product(string name, int inStock)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ShopExceptions("Incorrect product's name");
        if (inStock < 1) throw new ShopExceptions("Amount of products cannot be 0 or less");
        Name = name;
        InStock = inStock;
    }

    public string Name { get; }
    public decimal Price { get; private set; }
    public int InStock { get; private set; }

    public void ChangePrice(decimal newPrice)
    {
        if (newPrice < 1) throw new ShopExceptions("Price cannot be 0 or less");
        Price = newPrice;
    }

    public void AddNumberOfProducts(int amount)
    {
        if (amount < 1) throw new ShopExceptions("Can't add a negative number of products");
        InStock += amount;
    }

    public void RemoveNumberOfProducts(int amount)
    {
        if (amount < 1) throw new ShopExceptions("Can't remove a negative number of products");
        if (InStock < amount) throw new ShopExceptions("Insufficient amount of products in stock");
        InStock -= amount;
    }
}