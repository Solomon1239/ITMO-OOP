using System.IO;
using System.Net;
using Backups.Extra.Tools;

namespace Backups.Extra.Logger
{
    public class FileLogger : ILogger
    {
        private string _logFilePath;

        public FileLogger(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath)) throw new BackupExtraException("Incorrect log file");

            _logFilePath = filePath;
        }

        public LoggerType Type { get; } = LoggerType.File;

        public void Log(string message)
        {
            File.AppendAllText(_logFilePath, message);
        }
    }
}