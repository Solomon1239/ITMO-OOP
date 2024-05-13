using System.Collections.Generic;
using System.Linq;
using Backups.Extra.Entities;
using Backups.Extra.Tools;
using Backups.Models;

namespace Backups.Extra.Algorithms
{
    public class CleanupByCount : ICleanupAlgorithm
    {
        public CleanupByCount(int restorePointsAmount)
        {
            if (restorePointsAmount < 0) throw new BackupExtraException("Restore points amount cannot be less than 0");
            RestorePointsAmount = restorePointsAmount;
        }

        public int RestorePointsAmount { get; }

        public List<RestorePoint> FindRestorePointsToCleanup(BackupTaskExtra backupTaskExtra)
        {
            List<RestorePoint> restorePoints = backupTaskExtra.RestorePoints.Take(backupTaskExtra.RestorePoints.Count - RestorePointsAmount).ToList();
            return restorePoints;
        }

        public void CleanupRestorePoints(BackupTaskExtra backupTaskExtra)
        {
            List<RestorePoint> restorePoints = FindRestorePointsToCleanup(backupTaskExtra);

            restorePoints.ForEach(backupTaskExtra.RemoveRestorePoint);

            backupTaskExtra.Logger.Log("Restore points has been cleanup by count");
        }
    }
}