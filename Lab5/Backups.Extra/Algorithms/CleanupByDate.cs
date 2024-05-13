using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Extra.Entities;
using Backups.Models;

namespace Backups.Extra.Algorithms
{
    public class CleanupByDate : ICleanupAlgorithm
    {
        public CleanupByDate(TimeSpan storageInterval)
        {
            StorageInterval = storageInterval;
        }

        public TimeSpan StorageInterval { get; }

        public List<RestorePoint> FindRestorePointsToCleanup(BackupTaskExtra backupTaskExtra)
        {
            List<RestorePoint> restorePoints =
                backupTaskExtra.RestorePoints.Where(point => DateTime.Now.Subtract(point.Date) < StorageInterval).ToList();

            return restorePoints;
        }

        public void CleanupRestorePoints(BackupTaskExtra backupTaskExtra)
        {
            List<RestorePoint> restorePoints = FindRestorePointsToCleanup(backupTaskExtra);

            restorePoints.ForEach(backupTaskExtra.RemoveRestorePoint);

            backupTaskExtra.Logger.Log("Restore points has been cleanup by date");
        }
    }
}