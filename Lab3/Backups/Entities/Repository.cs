using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Backups.Entities;
using Backups.Tools;

namespace Backups.Models
{
    public class Repository : IRepository
    {
        private DirectoryInfo _directoryInfo;

        public Repository(string fullPath)
        {
            if (string.IsNullOrWhiteSpace(fullPath)) throw new BackupsException("Incorrect path");

            FullPath = Path.GetFullPath(fullPath);
            Name = Path.GetFileName(FullPath);
            _directoryInfo = new DirectoryInfo(fullPath);
        }

        public string FullPath { get; private set; }
        public string Name { get; }

        public void Read(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) throw new BackupsException("Incorrect path");
            FullPath = Path.GetFullPath(path);

            _directoryInfo = new DirectoryInfo(path);
        }

        public void Save(BackupTask backupTask, IArchiver archiver)
        {
            foreach (Storage storage in backupTask.RestorePoints.Last().Storages)
            {
                foreach (BackupObject backupObject in storage.BackupObjects)
                {
                    archiver.Archive(
                        $@"{backupTask.Repository.Name}\{backupTask.Name}\{backupObject.FileName}",
                        $@"{backupTask.Repository.Name}\{backupTask.Name}\{storage.Name}");
                }
            }
        }
    }
}