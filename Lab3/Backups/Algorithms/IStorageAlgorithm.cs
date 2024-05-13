using System.Collections.Generic;
using Backups.Models;

namespace Backups.Algorithms
{
    public interface IStorageAlgorithm
    {
        public List<Storage> StorageFiles(List<BackupObject> backupObjects, int archiveNumber);
    }
}