using System;
using System.Linq;
using Banks.Entities.Accounts;
using Banks.Entities.Banks;
using Banks.Entities.Banks.Transactions;
using Banks.Entities.Clients;
using Xunit;

namespace BanksTest
{
    public class BankTest
    {
        [Fact]
        public void WhenCentralBank_AndAddBank_ThenBankShouldBeCreated()
        {
            // Arrange.
            string name = "bank";
            decimal commission = 100;
            decimal percent = 2;
            decimal smallPercentage = 3;
            decimal averagePercentage = 3.5M;
            decimal largePercentage = 4;
            decimal creditLimit = 30000;
            decimal transferLimit = 1000;
            DateTime withdrawalUnlockDate = new DateTime(2023, 1, 1);
            BankConfig bankConfig = new BankConfig(name, commission, percent, smallPercentage, averagePercentage, largePercentage, creditLimit, transferLimit, withdrawalUnlockDate);
            Bank bank = new Bank(bankConfig);
            CentralBank centralBank = new CentralBank();

            // Act.
            centralBank.AddBank(bank);

            // Assert.
            Assert.Contains(bank, centralBank.Banks);
        }

        [Fact]
        public void WhenCentralBank_AndAddClient_ThenClientShouldBeCreated()
        {
            // Arrange.
            string clientName = "name";
            string clientSurname = "surname";
            int passportId = 111111;
            string address = "test address";
            Client client = new Client(clientName, clientSurname, passportId, address);
            string bankName = "bank";
            decimal commission = 100;
            decimal percent = 2;
            decimal smallPercentage = 3;
            decimal averagePercentage = 3.5M;
            decimal largePercentage = 4;
            decimal creditLimit = 30000;
            decimal transferLimit = 1000;
            DateTime withdrawalUnlockDate = new DateTime(2023, 1, 1);
            BankConfig bankConfig = new BankConfig(bankName, commission, percent, smallPercentage, averagePercentage, largePercentage, creditLimit, transferLimit, withdrawalUnlockDate);
            Bank bank = new Bank(bankConfig);
            CentralBank centralBank = new CentralBank();

            // Act.
            centralBank.AddBank(bank);
            centralBank.AddClient(client, bank);

            // Assert.
            Assert.Contains(client, centralBank.GetBank(bankName).ClientAccounts.Keys);
        }

        [Fact]
        public void WhenBank_AndAddCreditDebitDepositAccounts_ThenAccountsShouldBeCreated()
        {
            // Arrange.
            string clientName = "name";
            string clientSurname = "surname";
            int passportId = 111111;
            string address = "test address";
            Client client = new Client(clientName, clientSurname, passportId, address);
            string bankName = "bank";
            decimal commission = 100;
            decimal percent = 2;
            decimal smallPercentage = 3;
            decimal averagePercentage = 3.5M;
            decimal largePercentage = 4;
            decimal creditLimit = 30000;
            decimal transferLimit = 1000;
            DateTime withdrawalUnlockDate = new DateTime(2023, 1, 1);
            BankConfig bankConfig = new BankConfig(bankName, commission, percent, smallPercentage, averagePercentage, largePercentage, creditLimit, transferLimit, withdrawalUnlockDate);
            Bank bank = new Bank(bankConfig);
            CentralBank centralBank = new CentralBank();

            // Act.
            centralBank.AddBank(bank);
            centralBank.AddClient(client, bank);
            bank.CreateAccount(client, AccountType.Credit);
            bank.CreateAccount(client, AccountType.Debit);
            bank.CreateAccount(client, AccountType.Deposit);

            // Assert.
            Assert.Equal(3, bank.ClientAccounts[client].Count);
        }

        [Fact]
        public void WhenBank_AndDoReplenishmentTransaction_ThenTransactionValueShouldBeChanged()
        {
            // Arrange.
            string clientName = "name";
            string clientSurname = "surname";
            int passportId = 111111;
            string address = "test address";
            Client client = new Client(clientName, clientSurname, passportId, address);
            string bankName = "bank";
            decimal commission = 100;
            decimal percent = 2;
            decimal smallPercentage = 3;
            decimal averagePercentage = 3.5M;
            decimal largePercentage = 4;
            decimal creditLimit = 30000;
            decimal transferLimit = 1000;
            DateTime withdrawalUnlockDate = new DateTime(2023, 1, 1);
            BankConfig bankConfig = new BankConfig(bankName, commission, percent, smallPercentage, averagePercentage, largePercentage, creditLimit, transferLimit, withdrawalUnlockDate);
            Bank bank = new Bank(bankConfig);
            CentralBank centralBank = new CentralBank();

            // Act.
            centralBank.AddBank(bank);
            centralBank.AddClient(client, bank);
            bank.CreateAccount(client, AccountType.Debit);

            bank.DoTransaction(new Replenishment(500, bank.ClientAccounts[client].Last()),  client);

            // Assert.
            Assert.Equal(500, bank.ClientAccounts[client].Last().GetValue());
        }

        [Fact]
        public void WhenBank_AndDoWithdrawalTransaction_ThenTransactionValueShouldBeChanged()
        {
            // Arrange.
            string clientName = "name";
            string clientSurname = "surname";
            int passportId = 111111;
            string address = "test address";
            Client client = new Client(clientName, clientSurname, passportId, address);
            string bankName = "bank";
            decimal commission = 100;
            decimal percent = 2;
            decimal smallPercentage = 3;
            decimal averagePercentage = 3.5M;
            decimal largePercentage = 4;
            decimal creditLimit = 30000;
            decimal transferLimit = 1000;
            DateTime withdrawalUnlockDate = new DateTime(2023, 1, 1);
            BankConfig bankConfig = new BankConfig(bankName, commission, percent, smallPercentage, averagePercentage, largePercentage, creditLimit, transferLimit, withdrawalUnlockDate);
            Bank bank = new Bank(bankConfig);
            CentralBank centralBank = new CentralBank();

            // Act.
            centralBank.AddBank(bank);
            centralBank.AddClient(client, bank);
            bank.CreateAccount(client, AccountType.Debit);

            bank.DoTransaction(new Replenishment(500, bank.ClientAccounts[client].Last()),  client);
            bank.DoTransaction(new MoneyWithdrawal(250, bank.ClientAccounts[client].Last()),  client);

            // Assert.
            Assert.Equal(250, bank.ClientAccounts[client].Last().GetValue());
        }

        [Fact]
        public void WhenBank_AndDoTransferTransaction_ThenTransactionValueShouldBeChanged()
        {
            // Arrange.
            string clientName = "name";
            string clientSurname = "surname";
            int passportId = 111111;
            string address = "test address";
            Client client = new Client(clientName, clientSurname, passportId, address);
            string bankName = "bank";
            decimal commission = 100;
            decimal percent = 2;
            decimal smallPercentage = 3;
            decimal averagePercentage = 3.5M;
            decimal largePercentage = 4;
            decimal creditLimit = 30000;
            decimal transferLimit = 1000;
            DateTime withdrawalUnlockDate = new DateTime(2023, 1, 1);
            BankConfig bankConfig = new BankConfig(bankName, commission, percent, smallPercentage, averagePercentage, largePercentage, creditLimit, transferLimit, withdrawalUnlockDate);
            Bank bank = new Bank(bankConfig);
            CentralBank centralBank = new CentralBank();

            // Act.
            centralBank.AddBank(bank);
            centralBank.AddClient(client, bank);
            bank.CreateAccount(client, AccountType.Debit);
            bank.CreateAccount(client, AccountType.Debit);

            bank.DoTransaction(new Replenishment(500, bank.ClientAccounts[client][0]),  client);
            bank.DoTransaction(new MoneyTransfer(250, bank.ClientAccounts[client][0], bank.ClientAccounts[client][1]),  client);

            // Assert.
            Assert.Equal(250, bank.ClientAccounts[client][0].GetValue());
            Assert.Equal(250, bank.ClientAccounts[client][1].GetValue());
        }
    }
}