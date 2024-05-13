using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Backups.Entities;
using Backups.Models;

namespace Backups.Algorithms
{
    public class SingleStorage : IStorageAlgorithm
    {
        public List<Storage> StorageFiles(List<BackupObject> backupObjects, int archiveNumber)
        {
            List<Storage> storages = new List<Storage>();

            Storage storage = new Storage($"SingleArchive({archiveNumber})");

            foreach (BackupObject backupObject in backupObjects)
            {
                storage.AddBackupObject(new BackupObject($@"{backupObject.FileName}"));
            }

            storages.Add(storage);

            return storages;
        }
    }
}