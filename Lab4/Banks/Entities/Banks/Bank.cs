using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Entities.Accounts;
using Banks.Entities.Banks.Transactions;
using Banks.Entities.Clients;
using Banks.Observer;
using Banks.Tools;

namespace Banks.Entities.Banks
{
    public class Bank
    {
        private Dictionary<Client, List<IAccount>> _clientAccounts;
        private List<Transaction> _transactions = new List<Transaction>();

        public Bank(BankConfig bankConfig)
        {
            BankConfig = bankConfig;

            _clientAccounts = new Dictionary<Client, List<IAccount>>();
        }

        public IReadOnlyDictionary<Client, List<IAccount>> ClientAccounts => _clientAccounts;
        private BankConfig BankConfig { get; }
        public string GetName() => BankConfig.Name;

        public IAccount? FindAccount(Client client, Guid id)
        {
            return _clientAccounts[client].FirstOrDefault(account => account.Id == id);
        }

        public void DoTransaction(Transaction transaction, Client client)
        {
            if (!_clientAccounts.ContainsKey(client)) throw new BankException("Client does not exist");
            if (client.IsDoubtfulClient() && transaction.Value > BankConfig.TransferLimit)
                throw new BankException("You cannot complete the operation until you fill in the missing information");

            transaction.Execute();
        }

        public void CancelTransaction(Guid id)
        {
            Transaction? transaction = _transactions.FirstOrDefault(transaction => transaction.Id == id);
            if (transaction is null)
                throw new BankException("Transaction does not exist");

            transaction.Cancel();
        }

        public void CreateAccount(Client client, AccountType type)
        {
            Guid accountId = Guid.NewGuid();
            AccountCreator accountCreator = GetFactory(type, accountId);
            IAccount account = accountCreator.Create();

            if (!_clientAccounts.ContainsKey(client))
                AddClient(client);

            _clientAccounts[client].Add(account);
        }

        public void AddClient(Client client)
        {
            if (_clientAccounts.ContainsKey(client)) throw new BankException("Client already exist");

            _clientAccounts.Add(client, new List<IAccount>());
        }

        public void SubscribeClientToMailingList(Client client)
        {
            if (!client.Subscribed) client.ChangeSubscription();

            BankConfig.RegisterObserver(client);
        }

        public void UnsubscribeClientToMailingList(Client client)
        {
            if (client.Subscribed) client.ChangeSubscription();

            BankConfig.RemoveObserver(client);
        }

        private AccountCreator GetFactory(AccountType type, Guid id)
        {
            return type switch
            {
                AccountType.Credit => new CreditAccountCreator(BankConfig.Commission, BankConfig.CreditLimit, id),
                AccountType.Debit => new DebitAccountCreator(BankConfig.Percent, id),
                AccountType.Deposit => new DepositAccountCreator(BankConfig.SmallPercentage, BankConfig.AveragePercentage, BankConfig.LargePercentage, id, BankConfig.WithdrawalUnlockDate),
                _ => throw new BankException("Account type does not exist")
            };
        }
    }
}