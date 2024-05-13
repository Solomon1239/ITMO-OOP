using System.Collections.Generic;
using System.IO;
using Backups.Algorithms;
using Backups.Tools;

namespace Backups.Models
{
    public class Storage
    {
        private List<BackupObject> _backupObjects = new List<BackupObject>();

        public Storage(string storagePath)
        {
            FullPath = Path.GetFullPath(storagePath);
            Name = Path.GetFileName(FullPath);
        }

        public string FullPath { get; }
        public string Name { get; }

        public IReadOnlyCollection<BackupObject> BackupObjects => _backupObjects.AsReadOnly();

        public void AddBackupObject(BackupObject backupObject)
        {
            if (backupObject is null) throw new BackupsException("Empty object");

            _backupObjects.Add(backupObject);
        }
    }
}