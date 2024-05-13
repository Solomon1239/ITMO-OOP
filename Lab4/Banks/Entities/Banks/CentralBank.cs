#nullable enable
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using Banks.Entities.Accounts;
using Banks.Entities.Clients;
using Banks.Tools;

namespace Banks.Entities.Banks
{
    public class CentralBank
    {
        private List<Bank> _banks;
        private List<Client> _clients;

        public CentralBank()
        {
            _banks = new List<Bank>();
            _clients = new List<Client>();
        }

        public IReadOnlyCollection<Bank> Banks => _banks.AsReadOnly();
        public IReadOnlyCollection<Client> Clients => _clients.AsReadOnly();

        public void AddBank(Bank bank)
        {
            if (_banks.Contains(bank)) throw new BankException("Bank already exist");

            _banks.Add(bank);
        }

        public void AddClient(Client client, Bank bank)
        {
            if (!_banks.Contains(bank)) throw new BankException("Bank does not exist");
            if (_clients.Contains(client)) throw new BankException("Client already exist");

            _clients.Add(client);
            FindBank(bank.GetName())?.AddClient(client);
        }

        public Bank? FindBank(string name)
        {
            return _banks.FirstOrDefault(bank => bank.GetName() == name);
        }

        public Bank GetBank(string name)
        {
            Bank? bank = _banks.FirstOrDefault(bank => bank.GetName() == name);
            if (bank == null) throw new BankException("Bank does not exist");

            return bank;
        }

        public Client? FindClient(int passportId)
        {
            return _clients.FirstOrDefault(client => client.PassportId == passportId);
        }

        public Bank SpeedUpTime(string bankName, int monthCount)
        {
            Bank? bankForSpeedUp = _banks.Find(currentBank => currentBank.GetName() == bankName);

            for (int i = 0; i < monthCount; i++)
            {
                if (bankForSpeedUp?.ClientAccounts.Values == null) throw new BankException("Value is empty");
                foreach (IAccount account in bankForSpeedUp.ClientAccounts.Values)
                {
                    account.PercentageCalculation();
                    account.PaymentCalculation();
                }
            }

            return bankForSpeedUp ?? throw new BankException("Value is empty");
        }
    }
}