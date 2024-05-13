namespace Backups.Extra.Logger
{
    public interface ILogger
    {
        public LoggerType Type { get; }

        public void Log(string message);
    }
}