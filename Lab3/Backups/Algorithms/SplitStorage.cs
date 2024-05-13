using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Backups.Entities;
using Backups.Models;

namespace Backups.Algorithms
{
    public class SplitStorage : IStorageAlgorithm
    {
        public List<Storage> StorageFiles(List<BackupObject> backupObjects, int archiveNumber)
        {
            List<Storage> storages = new List<Storage>();

            foreach (BackupObject backupObject in backupObjects)
            {
                Storage storage = new Storage($"{backupObject.FileName}({archiveNumber})");

                storage.AddBackupObject(new BackupObject($@"{backupObject.FileName}"));
                storages.Add(storage);
            }

            return storages;
        }
    }
}