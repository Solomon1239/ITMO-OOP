using System;
using Banks.Tools;

namespace Banks.Entities.Accounts
{
    public class DepositAccount : IAccount
    {
        private const decimal AmountOfValueToSmallPercentage = 50000;
        private const decimal AmountOfValueToAveragePercentage = 100000;
        private decimal _value;
        private decimal _smallPercentage;
        private decimal _averagePercentage;
        private decimal _largePercentage;
        private decimal _percent;
        private DateTime _withdrawalUnlockDate;
        private decimal _monthlyPayment = 0;
        private bool _isFirstReplenishment = true;

        public DepositAccount(decimal smallPercentage, decimal averagePercentage, decimal largePercentage, Guid id, DateTime withdrawalUnlockDate)
        {
            if (smallPercentage < 0 || averagePercentage < 0 || largePercentage < 0)
                throw new AccountException("Percent cannot less than 0");
            if (withdrawalUnlockDate < DateTime.Now)
                throw new AccountException("You cannot set the deposit term earlier than now");

            _smallPercentage = smallPercentage;
            _averagePercentage = averagePercentage;
            _largePercentage = largePercentage;
            _withdrawalUnlockDate = withdrawalUnlockDate;
            Id = id;
        }

        public Guid Id { get; }

        public decimal GetValue() => _value;
        public void Withdrawal(decimal value)
        {
            if (value < 0) throw new AccountException("Value cannot less than 0");
            if (value > _value) throw new AccountException("You cannot withdraw more money than you have in your account");
            if (DateTime.Now < _withdrawalUnlockDate) throw new AccountException("You cannot withdraw money early");

            _value -= value;
        }

        public void Replenishment(decimal value)
        {
            if (value < 0) throw new AccountException("Value cannot less than 0");

            if (_isFirstReplenishment)
            {
                if (value < AmountOfValueToSmallPercentage)
                    _percent = _smallPercentage;
                else if (value < AmountOfValueToAveragePercentage)
                    _percent = _averagePercentage;
                else
                    _percent = _largePercentage;

                _isFirstReplenishment = false;
            }

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