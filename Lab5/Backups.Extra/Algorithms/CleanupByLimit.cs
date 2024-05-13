using System.Collections.Generic;
using System.Linq;
using Backups.Extra.Entities;
using Backups.Extra.Tools;
using Backups.Models;

namespace Backups.Extra.Algorithms
{
    public class CleanupByLimit : ICleanupAlgorithm
    {
        private List<ICleanupAlgorithm> _algorithms;

        public CleanupByLimit(List<ICleanupAlgorithm> algorithms, LimitType limitType)
        {
            _algorithms = algorithms;
            LimitType = limitType;
        }

        public IReadOnlyCollection<ICleanupAlgorithm> Algorithms => _algorithms.AsReadOnly();
        public LimitType LimitType { get; }

        public List<RestorePoint> FindRestorePointsToCleanup(BackupTaskExtra backupTaskExtra)
        {
            List<RestorePoint>? restorePoints = null;

            foreach (ICleanupAlgorithm cleanupAlgorithm in _algorithms)
            {
                List<RestorePoint> points = cleanupAlgorithm.FindRestorePointsToCleanup(backupTaskExtra);

                if (LimitType == LimitType.AtLeastOne)
                {
                    restorePoints = new List<RestorePoint>();
                    restorePoints.AddRange(points.Where(point => !restorePoints.Contains(point)));
                }
                else if (LimitType == LimitType.ForAll)
                {
                    if (restorePoints == null)
                    {
                        restorePoints = new List<RestorePoint>();
                        restorePoints = points;
                    }

                    foreach (RestorePoint restorePoint in restorePoints)
                    {
                        if (!points.Contains(restorePoint)) restorePoints.Remove(restorePoint);
                    }
                }
            }

            return restorePoints ?? throw new BackupExtraException("No suitable restore points");
        }

        public void CleanupRestorePoints(BackupTaskExtra backupTaskExtra)
        {
            List<RestorePoint> restorePoints = FindRestorePointsToCleanup(backupTaskExtra);

            restorePoints.ForEach(backupTaskExtra.RemoveRestorePoint);

            backupTaskExtra.Logger.Log($"Restore points has been cleanup by {LimitType}");
        }
    }
}