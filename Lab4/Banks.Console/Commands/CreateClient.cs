using System;
using Banks.Entities.Banks;
using Banks.Entities.Clients;
using Banks.Tools;

namespace BanksConsole.Commands
{
    public class CreateClient : ICommand
    {
        private CentralBank _centralBank = CreateCentralBank.CentralBank;

        public void Execute()
        {
            ClientBuilder clientBuilder = new ClientBuilder();

            Console.WriteLine("Enter bank name from the list: ");
            WriteAllBanks();
            string bankName = Console.ReadLine() ?? throw new BankException("Incorrect bank name");
            Bank bank = _centralBank.FindBank(bankName) ?? throw new BankException("Bank does not exist");

            Console.WriteLine("Enter name");
            string clientName = Console.ReadLine() ?? throw new BankException("Incorrect client name");
            clientBuilder.SetName(clientName);

            Console.WriteLine("Enter surname");
            string clientSurname = Console.ReadLine() ?? throw new BankException("Incorrect surname");
            clientBuilder.SetSurname(clientSurname);

            Console.WriteLine("Do you want to enter passport data? Enter: yes/no");
            if (Console.ReadLine() == "yes")
            {
                Console.WriteLine("Enter passport id");
                while (true)
                {
                    string passportIdEntry = Console.ReadLine() ?? throw new BankException("Incorrect Id");
                    if (int.TryParse(passportIdEntry, out int id))
                    {
                        int passportId = id;
                        clientBuilder.SetPassportId(passportId);

                        break;
                    }

                    Console.WriteLine("Incorrect passport id, please enter it again");
                }
            }

            Console.WriteLine("Do you want to enter address? Enter: yes/no");
            if (Console.ReadLine() == "yes")
            {
                Console.WriteLine("Enter address");
                string address = Console.ReadLine() ?? throw new BankException("Incorrect adress");
                clientBuilder.SetAddress(address);
            }

            Client client = clientBuilder.Build();

            _centralBank.AddClient(client, bank);
            _centralBank.FindBank(bankName)?.AddClient(client);
            Console.WriteLine("Client successfully created");
        }

        private void WriteAllBanks()
        {
            int index = 1;
            foreach (Bank bank in _centralBank.Banks)
            {
                Console.WriteLine($"{index++}) {bank.GetName()}");
            }
        }
    }
}