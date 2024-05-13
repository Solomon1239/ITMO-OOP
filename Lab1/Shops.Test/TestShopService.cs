using Shops.Entities;
using Shops.Models;
using Shops.Services;
using Xunit;

namespace Shops.Test;

public class TestShopService
{
    private ShopService _service = new ShopService();
    [Fact]
    public void SupplyOfProductsToTheShop()
    {
        Shop newShop = _service.AddShop("Перекресток", "просп. Энгельса, 33, корп. 1");
        const int amountOfWaterInShop = 25;
        const int waterPrice = 45;
        Product water = _service.AddProduct("Вода", waterPrice, amountOfWaterInShop, newShop);
        const int amountOfKitKatInShop = 30;
        const int kitKatPrice = 55;
        Product kitKat = _service.AddProduct("KitKat", kitKatPrice, amountOfKitKatInShop, newShop);
        newShop.DeliveryProducts(new List<Product>() { water, kitKat });
        Assert.Equal(newShop.FindProduct(water).InStock, amountOfWaterInShop);
        Assert.Equal(newShop.FindProduct(kitKat).InStock, amountOfKitKatInShop);
    }

    [Fact]
    public void SettingAndChangingThePriceOfAProduct()
    {
        Shop newShop = _service.AddShop("Перекресток", "просп. Энгельса, 33, корп. 1");
        const int amountOfWaterInShop = 25;
        const int price = 45;
        Product water = _service.AddProduct("Вода", price, amountOfWaterInShop, newShop);
        newShop.AddProduct(water);
        Assert.Equal(newShop.FindProduct(water).Price, price);
        const int newPrice = 50;
        newShop.FindProduct(water).ChangePrice(newPrice);
        Assert.Equal(newShop.FindProduct(water).Price, newPrice);
    }

    [Fact]
    public void SearchForAStoreWhereABatchOfProductsCanBeBoughtCheaply()
    {
        Shop shop1 = _service.AddShop("Перекресток", "просп. Энгельса, 33, корп. 1");
        const int waterAmount1 = 25;
        const int waterPrice1 = 45;
        shop1.AddProduct(_service.AddProduct("Вода", waterPrice1, waterAmount1, shop1));
        const int kitKatAmount1 = 40;
        const int kitKatPrice1 = 50;
        shop1.AddProduct(_service.AddProduct("KitKat", kitKatPrice1, kitKatAmount1, shop1));

        Shop shop2 = _service.AddShop("Перекресток", "Каменноостровский просп., 10");
        const int amount2 = 30;
        const int price2 = 60;
        shop2.AddProduct(_service.AddProduct("Вода", price2, amount2, shop2));
        const int kitKatAmount2 = 20;
        const int kitKatPrice2 = 35;
        shop2.AddProduct(_service.AddProduct("KitKat", kitKatPrice2, kitKatAmount2, shop2));

        Shop shop3 = _service.AddShop("Перекресток", "Аптекарский просп., 18");
        const int amount3 = 15;
        const int price3 = 25;
        shop3.AddProduct(_service.AddProduct("Вода", price3, amount3, shop3));
        const int kitKatAmount3 = 30;
        const int kitKatPrice3 = 40;
        shop3.AddProduct(_service.AddProduct("KitKat", kitKatPrice3, kitKatAmount3, shop3));

        const int neededAmountOfWaterInShop = 20;
        const int neededAmountOfKitKatInShop = 10;
        Assert.Equal(
            _service.FindCheapShop(
            new List<Product>() { new Product("Вода", neededAmountOfWaterInShop),  new Product("KitKat", neededAmountOfKitKatInShop) }), shop1);
    }

    [Fact]
    public void BuyingABatchOfProductsInAStore()
    {
        Shop newShop = _service.AddShop("Перекресток", "просп. Энгельса, 33, корп. 1");
        Customer newCustomer = _service.AddCustomer("Иван");
        const int amountOfWaterInShop = 25;
        const int price = 45;
        Product water = _service.AddProduct("Вода", price, amountOfWaterInShop, newShop);
        newShop.AddProduct(water);
        const int neededAmountOfWaterInShop = 2;
        const int replenishmentAmount = 5000;
        newCustomer.Replenishment(replenishmentAmount);
        _service.BuyProducts(new List<Product>() { new Product("Вода", neededAmountOfWaterInShop) }, newShop, newCustomer);
        Assert.Equal(newCustomer.Money, replenishmentAmount - (price * neededAmountOfWaterInShop));
        Assert.Equal(
            newShop.FindProduct(water).InStock,
            amountOfWaterInShop - neededAmountOfWaterInShop);
    }
}