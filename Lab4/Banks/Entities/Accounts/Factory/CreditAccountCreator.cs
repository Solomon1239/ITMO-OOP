using System;

namespace Banks.Entities.Accounts
{
    public class CreditAccountCreator : AccountCreator
    {
        private decimal _commission;
        private decimal _creditLimit;
        private Guid _id;
        public CreditAccountCreator(decimal commission, decimal creditLimit, Guid id)
        {
            _commission = commission;
            _creditLimit = creditLimit;
            _id = id;
        }

        public override IAccount Create()
        {
            CreditAccount account = new CreditAccount(_commission, _creditLimit, _id);
            return account;
        }
    }
}