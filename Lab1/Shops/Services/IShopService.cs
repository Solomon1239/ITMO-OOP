using Shops.Entities;
using Shops.Models;

namespace Shops.Services;

public interface IShopService
{
    Shop AddShop(string name, string address);
    Customer AddCustomer(string name);
    Product AddProduct(string name, decimal price, int inStock, Shop shop);
    Shop FindCheapShop(List<Product> products);
    void BuyProducts(List<Product> products, Shop curShop, Customer customer);
}