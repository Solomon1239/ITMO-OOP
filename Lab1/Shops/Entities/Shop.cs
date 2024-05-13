using Shops.Models;

namespace Shops.Entities;

public class Shop
{
    public Shop(string name, Guid id, string address)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ShopExceptions("Incorrect shop's name");

        if (string.IsNullOrWhiteSpace(address)) throw new ShopExceptions("Incorrect address");

        Name = name;
        ID = id;
        Address = address;
        Products = new List<Product>();
    }

    public string Name { get; }
    public Guid ID { get; }
    public string Address { get; }
    public List<Product> Products { get; }

    public void ChangePrice(Product curProduct, decimal newPrice)
    {
        Product? product = Products.FirstOrDefault(product => product.Name == curProduct.Name);
        if (product is null)
            throw new ShopExceptions("There is no such product in the shop");
        product.ChangePrice(newPrice);
    }

    public void AddProduct(Product newProduct)
    {
        Product? curProduct = Products.FirstOrDefault(curProduct => curProduct.Name == newProduct.Name);
        if (curProduct is null)
        {
            Products.Add(newProduct);
        }
        else
        {
            curProduct.ChangePrice(newProduct.Price);
            curProduct.AddNumberOfProducts(newProduct.InStock);
        }
    }

    public void RemoveProduct(Product neededProduct)
    {
        Product? curProduct = Products.FirstOrDefault(curProduct => curProduct.Name == neededProduct.Name);
        if (curProduct is null)
            throw new ShopExceptions("This product is out of stock");
        if (curProduct.InStock < neededProduct.InStock)
            throw new ShopExceptions("The required number of products is not in stock");

        curProduct.RemoveNumberOfProducts(neededProduct.InStock);
        if (curProduct.InStock == 0)
            Products.Remove(curProduct);
    }

    public bool AllRequiredProductsInStock(List<Product> neededProducts)
    {
        return neededProducts.All(product => Products.Any(each => each.Name == product.Name));
    }

    public bool TheRightAmountOfProductsInStock(List<Product> neededProducts)
    {
        return neededProducts.All(product => Products.Any(each => each.InStock >= product.InStock && each.Name == product.Name));
    }

    public void DeliveryProducts(List<Product> products)
    {
        products.ForEach(product => AddProduct(product));
    }

    public Product FindProduct(Product neededProduct)
    {
        return Products.First(product => product.Name == neededProduct.Name);
    }
}