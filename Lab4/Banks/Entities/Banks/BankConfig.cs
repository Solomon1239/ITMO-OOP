using System;
using System.Collections.Generic;
using Banks.Observer;
using Banks.Tools;

namespace Banks.Entities.Banks
{
    public class BankConfig : IObservable
    {
         private List<IObserver> _observers;
         public BankConfig(
             string name,
             decimal commission,
             decimal percent,
             decimal smallPercentage,
             decimal averagePercentage,
             decimal largePercentage,
             decimal creditLimit,
             decimal transferLimit,
             DateTime withdrawalUnlockDate)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new BankException("Incorrect bank name");
            if (commission < 0) throw new BankException("Commission cannot less than 0");
            if (percent < 0 || smallPercentage < 0 || averagePercentage < 0 || largePercentage < 0)
                throw new BankException("Percent cannot less than 0");
            if (transferLimit < 0) throw new BankException("Transfer cannot less than 0");

            Name = name;
            Commission = commission;
            Percent = percent;
            SmallPercentage = smallPercentage;
            AveragePercentage = averagePercentage;
            LargePercentage = largePercentage;
            CreditLimit = creditLimit;
            TransferLimit = transferLimit;
            WithdrawalUnlockDate = withdrawalUnlockDate;

            _observers = new List<IObserver>();
        }

         public string Name { get; }
         public decimal Commission { get; private set; }
         public decimal Percent { get; private set; }
         public decimal SmallPercentage { get; private set; }
         public decimal AveragePercentage { get; private set; }
         public decimal LargePercentage { get; private set; }
         public decimal CreditLimit { get; private set; }
         public decimal TransferLimit { get; private set; }
         public DateTime WithdrawalUnlockDate { get; }

         public void ChangeCommission(decimal commission)
        {
            if (commission < 0) throw new BankException("Commission cannot less than 0");

            _observers.ForEach(observer => observer.Update($"Commission has been changed: previous - {Commission} now - {commission}"));

            Commission = commission;
        }

         public void ChangePercent(decimal percent)
        {
            if (percent < 0) throw new BankException("Percent cannot less than 0");

            _observers.ForEach(observer => observer.Update($"Percent has been changed: previous - {Percent} now - {percent}"));

            Percent = percent;
        }

         public void ChangeSmallPercentage(decimal smallPercentage)
        {
            if (smallPercentage < 0) throw new BankException("Percent cannot less than 0");

            _observers.ForEach(observer => observer.Update($"Percent has been changed: previous - {SmallPercentage} now - {smallPercentage}"));

            SmallPercentage = smallPercentage;
        }

         public void ChangeAveragePercentage(decimal averagePercentage)
        {
            if (averagePercentage < 0) throw new BankException("Percent cannot less than 0");

            _observers.ForEach(observer => observer.Update($"Percent has been changed: previous - {AveragePercentage} now - {averagePercentage}"));

            AveragePercentage = averagePercentage;
        }

         public void ChangeLargePercentage(decimal largePercentage)
        {
            if (largePercentage < 0) throw new BankException("Percent cannot less than 0");

            _observers.ForEach(observer => observer.Update($"Percent has been changed: previous - {LargePercentage} now - {largePercentage}"));

            LargePercentage = largePercentage;
        }

         public void ChangeCreditLimit(decimal creditLimit)
        {
            _observers.ForEach(observer => observer.Update($"Credit limit has been changed: previous - {CreditLimit} now - {creditLimit}"));

            CreditLimit = creditLimit;
        }

         public void ChangeTransferLimit(decimal transferLimit)
        {
            if (transferLimit < 0) throw new BankException("Transfer limit cannot less than 0");

            _observers.ForEach(observer => observer.Update($"Transfer limit has been changed: previous - {TransferLimit} now - {transferLimit}"));

            TransferLimit = transferLimit;
        }

         public void RegisterObserver(IObserver observer)
        {
            if (_observers.Contains(observer)) throw new BankException("Observer already exist");

            _observers.Add(observer);
        }

         public void RemoveObserver(IObserver observer)
        {
            if (!_observers.Contains(observer)) throw new BankException("Observer does not exist");

            _observers.Remove(observer);
        }

         public void NotifyObservers(string message)
        {
            foreach (IObserver observer in _observers)
            {
                observer.Update(message);
            }
        }
    }
}
