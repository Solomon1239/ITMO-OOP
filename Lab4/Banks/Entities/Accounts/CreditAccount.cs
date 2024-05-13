using System;
using Banks.Tools;

namespace Banks.Entities.Accounts
{
    public class CreditAccount : IAccount
    {
        private decimal _value = 0;
        private decimal _commission;
        private decimal _creditLimit;
        private decimal _monthlyPayment = 0;

        public CreditAccount(decimal commission, decimal creditLimit, Guid id)
        {
            if (commission < 0) throw new AccountException("Commission cannot less than 0");

            _commission = commission;
            _creditLimit = -Math.Abs(creditLimit);
            Id = id;
        }

        public Guid Id { get; }

        public decimal GetValue() => _value;
        public void Withdrawal(decimal value)
        {
            if (value < 0) throw new AccountException("Value cannot less than 0");
            if (_value - value < _creditLimit) throw new AccountException("You cannot exceed the withdrawal limit");

            _value -= value;
        }

        public void Replenishment(decimal value)
        {
            if (value < 0) throw new AccountException("Value cannot less than 0");

            _value += value;
        }

        public void PaymentCalculation()
        {
            Withdrawal(_monthlyPayment);

            _monthlyPayment = 0;
        }

        public void PercentageCalculation()
        {
            _monthlyPayment += _commission;
        }
    }
}