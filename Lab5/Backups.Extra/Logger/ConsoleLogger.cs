using System;

namespace Backups.Extra.Logger
{
    public class ConsoleLogger : ILogger
    {
        public LoggerType Type { get; } = LoggerType.Console;

        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}