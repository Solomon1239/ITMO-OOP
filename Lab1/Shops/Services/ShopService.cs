using Shops.Entities;
using Shops.Models;

namespace Shops.Services;

public class ShopService : IShopService
{
    private List<Shop> _shops = new List<Shop>();
    public Shop AddShop(string name, string address)
    {
        var newShop = new Shop(name, Guid.NewGuid(), address);
        _shops.Add(newShop);
        return newShop;
    }

    public Customer AddCustomer(string name)
    {
        var newCustomer = new Customer(name);
        return newCustomer;
    }

    public Product AddProduct(string name, decimal price, int inStock, Shop shop)
    {
        var newProduct = new Product(name, price, inStock);
        return newProduct;
    }

    public Shop FindCheapShop(List<Product> products)
    {
        if (products == null)
            throw new ShopExceptions("There are no products in the list");
        var shopsWithNeededProducts = _shops.Where(shop =>
            shop.AllRequiredProductsInStock(products)).ToList();
        if (shopsWithNeededProducts == null)
            throw new ShopExceptions("There is no shop with the necessary products");
        if (shopsWithNeededProducts.Where(shop => shop.TheRightAmountOfProductsInStock(products)) == null)
            throw new ShopExceptions("There are no shops with the right amount of products");
        Shop? searchedStore = null;
        decimal lowestTotalPrice = decimal.MaxValue;
        foreach (var shop in shopsWithNeededProducts)
        {
            decimal sum = products.Sum(product => shop.Products.First(each => each.Name == product.Name).Price * product.InStock);
            if (sum > lowestTotalPrice || shop.TheRightAmountOfProductsInStock(products) == false)
                continue;
            lowestTotalPrice = sum;
            searchedStore = shop;
        }

        return searchedStore ?? throw new ShopExceptions("No suitable store");
    }

    public void BuyProducts(List<Product> products, Shop curShop, Customer customer)
    {
        decimal totalCost = products.Sum(product => product.InStock * curShop.Products.First(cur => cur.Name == product.Name).Price);
        customer.WriteOffFunds(totalCost);
        products.ForEach(product => curShop.RemoveProduct(product));
    }
}