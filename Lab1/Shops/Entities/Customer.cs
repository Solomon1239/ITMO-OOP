using Shops.Models;

namespace Shops.Entities;

public class Customer
{
    public Customer(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ShopExceptions("Incorrect customer's name");
        Name = name;
        Money = 0;
    }

    public string Name { get; }
    public decimal Money { get; private set; }

    public void Replenishment(decimal amount)
    {
        if (amount < 1) throw new ShopExceptions("replenishment amount cannot be 0 or less");
        Money += amount;
    }

    public void WriteOffFunds(decimal amount)
    {
        if (amount < 0)
            throw new ShopExceptions("You can't withdraw a negative amount from the account");
        if (Money < amount)
            throw new ShopExceptions("There are not enough funds in your account");
        Money -= amount;
    }
}