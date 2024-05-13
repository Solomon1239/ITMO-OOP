using System;
using Banks.Entities.Accounts;
using Banks.Tools;

namespace Banks.Entities.Banks.Transactions
{
    public abstract class Transaction
    {
        public Transaction(decimal value, IAccount account)
        {
            if (value < 0) throw new AccountException("Value cannot less than 0");

            Value = value;
            Account = account;
            CanselIsAvailable = true;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }
        public decimal Value { get; }
        public IAccount Account { get; }
        public bool CanselIsAvailable { get; protected set; }
        public abstract void Execute();
        public abstract void Cancel();
    }
}