using System;
using Banks.Entities.Accounts;
using Banks.Entities.Banks;
using Banks.Entities.Banks.Transactions;
using Banks.Entities.Clients;
using Banks.Tools;

namespace BanksConsole.Commands
{
    public class DoTransaction : ICommand
    {
        private CentralBank _centralBank = CreateCentralBank.CentralBank;
        public void Execute()
        {
            Console.WriteLine("Enter client passport id");
            int passportId = Convert.ToInt32(Console.ReadLine());
            Client client = _centralBank.FindClient(passportId) ?? throw new BankException("Incorrect client");
            Console.WriteLine("Enter bankName");
            string bankName = Console.ReadLine() ?? throw new BankException("Incorrect bank name");
            Bank bank = _centralBank.FindBank(bankName) ?? throw new BankException("Bank does not exist");
            Console.WriteLine("Enter account id from List: ");
            WriteClientAccountId(client, bank);
            IAccount account = bank.FindAccount(client, Guid.Parse(Console.ReadLine() ?? throw new BankException("Incorrect Id")))
                               ?? throw new BankException("Incorrect account");
            Console.WriteLine("Enter the transaction amount");
            decimal transactionValue = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter transaction type: Replenishment Transfer Withdrawal");
            switch (Console.ReadLine())
            {
                case "Replenishment":
                    bank.DoTransaction(new Replenishment(transactionValue, account), client);
                    break;
                case "Transfer":
                    Console.WriteLine("Enter receiver passport id");
                    int receiverPassportId = Convert.ToInt32(Console.ReadLine());
                    Client receiver = _centralBank.FindClient(receiverPassportId) ?? throw new BankException("Incorrect client");
                    Console.WriteLine("Enter receiver account id");
                    IAccount receiverAccount = bank.FindAccount(client, Guid.Parse(Console.ReadLine() ?? throw new BankException("Incorrect Id")))
                                               ?? throw new BankException("Incorrect receiver account");
                    bank.DoTransaction(new MoneyTransfer(transactionValue, account, receiverAccount), client);
                    break;
                case "Withdrawal":
                    bank.DoTransaction(new MoneyWithdrawal(transactionValue, account), client);
                    break;
                default:
                    Console.WriteLine("Non-existent transaction");
                    break;
            }

            Console.WriteLine("Transaction successfully done");
        }

        private void WriteClientAccountId(Client client, Bank bank)
        {
            int accountNumber = 1;
            Bank neededBank = _centralBank.FindBank(bank.GetName()) ?? throw new BankException("Value is empty");
            foreach (IAccount account in neededBank.ClientAccounts[client])
            {
                Console.WriteLine($"{accountNumber++}) {account.Id}");
            }
        }
    }
}