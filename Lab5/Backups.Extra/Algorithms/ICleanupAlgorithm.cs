using System.Collections.Generic;
using Backups.Extra.Entities;
using Backups.Models;

namespace Backups.Extra.Algorithms
{
    public interface ICleanupAlgorithm
    {
        List<RestorePoint> FindRestorePointsToCleanup(BackupTaskExtra backupTaskExtra);
        void CleanupRestorePoints(BackupTaskExtra backupTaskExtra);
    }
}