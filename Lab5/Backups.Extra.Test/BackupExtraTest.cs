using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Algorithms;
using Backups.Entities;
using Backups.Extra.Algorithms;
using Backups.Extra.Entities;
using Backups.Extra.Logger;
using Backups.Models;
using Xunit;

namespace Backups.ExtraTests
{
    public class BackupExtraTest
    {
        [Fact]
        public void WhenBackupTaskExtra_AndCreate3RestorePointThanDoCleanupByLimit_ThenRestorePointShouldBe2()
        {
            // Arrange.
            IArchiver archiverGz = new ArchiverGz();
            Repository repository = new Repository(@"repository");
            List<ICleanupAlgorithm> cleanupAlgorithms = new List<ICleanupAlgorithm>()
                { new CleanupByCount(2), new CleanupByDate(new TimeSpan(1, 0, 0)) };
            BackupTaskExtra backupTaskExtra = new BackupTaskExtra("backupSplit", new SplitStorage(), new Repository(@"repository"), archiverGz, new CleanupByLimit(cleanupAlgorithms, LimitType.ForAll), LoggerType.Console);
            BackupObject backupObject1 = new BackupObject($@"{repository.Name}\{backupTaskExtra.Name}\1.txt");
            BackupObject backupObject2 = new BackupObject($@"{repository.Name}\{backupTaskExtra.Name}\2.txt");
            DateTime dateTime = DateTime.Now;
            dateTime = dateTime.AddHours(-3);

            // Act.
            backupTaskExtra.AddBackupObject(backupObject1);
            backupTaskExtra.AddBackupObject(backupObject2);
            backupTaskExtra.CreateBackup();

            backupTaskExtra.RemoveBackupObject(backupObject2);
            backupTaskExtra.CreateBackup();

            backupTaskExtra.AddBackupObject(backupObject2);
            backupTaskExtra.CreateBackup();
            backupTaskExtra.RestorePoints.Last().SetCreationDate(dateTime);

            backupTaskExtra.CleanupAlgorithm.CleanupRestorePoints(backupTaskExtra);

            // Assert.
            Assert.Equal(2, backupTaskExtra.RestorePoints.Count);
        }
    }
}