using System;
using Banks.Tools;

namespace Banks.Entities.Accounts
{
    public class DebitAccount : IAccount
    {
        private decimal _value = 0;
        private decimal _percent;
        private decimal _monthlyPayment = 0;

        public DebitAccount(decimal percent, Guid id)
        {
            if (percent < 0) throw new AccountException("Percent cannot less than 0");

            _percent = percent;
            Id = id;
        }

        public Guid Id { get; }

        public decimal GetValue() => _value;
        public void Withdrawal(decimal value)
        {
            if (value < 0) throw new AccountException("Value cannot less than 0");
            if (value > _value) throw new AccountException("You cannot withdraw more money than you have in your account");

            _value -= value;
        }

        public void Replenishment(decimal value)
        {
            if (value < 0) throw new AccountException("Value cannot less than 0");

            _value += value;
        }

        public void PaymentCalculation()
        {
            Replenishment(_monthlyPayment);

            _monthlyPayment = 0;
        }

        public void PercentageCalculation()
        {
            decimal daysInYear = DateTime.IsLeapYear(DateTime.Now.Year) ? 366 : 365;

            _monthlyPayment += _value * (_percent / daysInYear);
        }
    }
}