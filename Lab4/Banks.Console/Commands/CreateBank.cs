using System;
using Banks.Entities.Banks;
using Banks.Tools;

namespace BanksConsole.Commands
{
    public class CreateBank : ICommand
    {
        private CentralBank _centralBank = CreateCentralBank.CentralBank;

        public void Execute()
        {
            Console.WriteLine("Enter bank name");
            string? name = Console.ReadLine() ?? throw new BankException("Incorrrect name");
            Console.WriteLine("Enter commission");
            decimal commission = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter percent");
            decimal percent = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter small percentage");
            decimal smallPercentage = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter average percentage");
            decimal averagePercentage = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter large percentage");
            decimal largePercentage = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter credit limit");
            decimal creditLimit = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter transfer limit");
            decimal transferLimit = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter withdrawal unlock date: 5/1/2008 8:30:52 AM");
            DateTime withdrawalUnlockDate = DateTime.Parse(Console.ReadLine() ?? throw new BankException("Incorrect date"));

            BankConfig bankConfig = new BankConfig(
                name,
                commission,
                percent,
                smallPercentage,
                averagePercentage,
                largePercentage,
                creditLimit,
                transferLimit,
                withdrawalUnlockDate);
            Bank bank = new Bank(bankConfig);

            _centralBank.AddBank(bank);
            Console.WriteLine("Bank successfully created");
        }
    }
}