using System;
using Banks.Entities.Banks;
using BanksConsole.Commands;

namespace BanksConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Welcome to BankApp\n You can run the following commands: \n" +
                              "createCentralBank\n" +
                              "createBank\n" +
                              "createClient\n" +
                              "createAccount\n" +
                              "doTransaction\n");

            bool breaker = true;
            while (breaker)
            {
                switch (Console.ReadLine())
                {
                    case "createCentralBank":
                        CreateCentralBank createCentralBank = new CreateCentralBank();
                        createCentralBank.Execute();
                        break;
                    case "createBank":
                        CreateBank createBank = new CreateBank();
                        createBank.Execute();
                        break;
                    case "createClient":
                        CreateClient createClient = new CreateClient();
                        createClient.Execute();
                        break;
                    case "createAccount":
                        CreateAccount createAccount = new CreateAccount();
                        createAccount.Execute();
                        break;
                    case "doTransaction":
                        DoTransaction doTransaction = new DoTransaction();
                        doTransaction.Execute();
                        break;
                    default:
                        breaker = false;
                        break;
                }
            }
        }
    }
}