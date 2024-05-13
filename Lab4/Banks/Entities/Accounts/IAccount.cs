using System;

namespace Banks.Entities.Accounts
{
    public interface IAccount
    {
        public Guid Id { get; }

        public decimal GetValue();
        public void Withdrawal(decimal value);
        public void Replenishment(decimal value);
        public void PaymentCalculation();
        public void PercentageCalculation();
    }
}