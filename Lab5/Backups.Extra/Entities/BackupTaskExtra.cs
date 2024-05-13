using System.Linq;
using Backups.Algorithms;
using Backups.Entities;
using Backups.Extra.Algorithms;
using Backups.Extra.Logger;
using Backups.Extra.Tools;
using Backups.Models;

namespace Backups.Extra.Entities
{
    public class BackupTaskExtra : BackupTask
    {
        public BackupTaskExtra(
            string name,
            IStorageAlgorithm storageAlgorithm,
            IRepository repository,
            IArchiver archiver,
            ICleanupAlgorithm cleanupAlgorithm,
            LoggerType loggerType,
            string? logFile = null)
            : base(name, storageAlgorithm, repository, archiver)
        {
            CleanupAlgorithm = cleanupAlgorithm;
            Logger = new ConsoleLogger();
            switch (loggerType)
            {
                case LoggerType.Console:
                    Logger = new ConsoleLogger();
                    break;
                case LoggerType.File:
                    Logger = new FileLogger(logFile ?? throw new BackupExtraException("Incorrect logFile"));
                    break;
            }
        }

        public ICleanupAlgorithm CleanupAlgorithm { get; private set; }
        public ILogger Logger { get; }

        public void MergeRestorePoint(RestorePoint oldPoint, RestorePoint newPoint)
        {
            if (StorageAlgorithm.GetType() == new SingleStorage().GetType()) RemoveRestorePoint(oldPoint);

            foreach (Storage pointStorage in oldPoint.Storages)
            {
                if (!newPoint.Storages.Contains(pointStorage)) newPoint.AddStorage(pointStorage);
            }

            Logger.Log("Points has been merged");
        }

        public void SetCleanupAlgorithm(ICleanupAlgorithm cleanupAlgorithm)
        {
            CleanupAlgorithm = cleanupAlgorithm;

            Logger.Log("Cleanup algorithm has been changed");
        }
    }
}