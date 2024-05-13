using System;

namespace Backups.Extra.Tools
{
    public class BackupExtraException : Exception
    {
        public BackupExtraException(string message)
            : base(message)
        {
        }
    }
}