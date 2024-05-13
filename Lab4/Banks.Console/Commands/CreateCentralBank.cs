using System;
using Banks.Entities.Banks;

namespace BanksConsole.Commands
{
    public class CreateCentralBank : ICommand
    {
        public static CentralBank CentralBank { get; set; } = new CentralBank();

        public void Execute()
        {
            Console.WriteLine("Central bank successfully created");
        }
    }
}