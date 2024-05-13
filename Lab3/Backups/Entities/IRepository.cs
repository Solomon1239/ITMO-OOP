using System.Collections.Generic;
using Backups.Entities;

namespace Backups.Models
{
    public interface IRepository
    {
        string Name { get; }
        void Read(string path);

        void Save(BackupTask backupTask, IArchiver archiver);
    }
}