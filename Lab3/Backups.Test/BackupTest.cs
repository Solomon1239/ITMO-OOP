using System;
using System.IO;
using System.Linq;
using Backups.Algorithms;
using Backups.Entities;
using Backups.Models;
using Xunit;

namespace BackupsTests
{
    public class BackupTest
    {
        [Fact]
        public void WhenBackupTask_AndAdd2BackupObjectsWithSplitStorageDelete1BackupObject_ThenRestorePointShouldBe2StorageShouldBe3()
        {
            // Arrange.
            IArchiver archiverGz = new ArchiverGz();
            Repository repository = new Repository(@"repository");
            BackupTask backupTask = new BackupTask("backupSplit", new SplitStorage(), new Repository(@"repository"), archiverGz);
            BackupObject backupObject1 = new BackupObject($@"{repository.Name}\{backupTask.Name}\1.txt");
            BackupObject backupObject2 = new BackupObject($@"{repository.Name}\{backupTask.Name}\2.txt");

            // Act.
            backupTask.AddBackupObject(backupObject1);
            backupTask.AddBackupObject(backupObject2);
            backupTask.CreateBackup();

            backupTask.RemoveBackupObject(backupObject2);
            backupTask.CreateBackup();

            // Assert.
            Assert.Equal(2, backupTask.RestorePoints.Count);
            Assert.Equal(3, backupTask.RestorePoints.Sum(point => point.Storages.Count));
        }

        [Fact]
        public void WhenBackupTask_AndAdd2BackupObjectsWithSingleStorage_ThenFoldersAndFilesShouldBeCreated()
        {
            // Arrange.
            IArchiver archiverGz = new ArchiverGz();
            Repository repository = new Repository(@"repository");
            BackupTask backupTask = new BackupTask("backupSingle", new SingleStorage(), repository, archiverGz);
            BackupObject backupObject1 = new BackupObject($@"{repository.Name}\{backupTask.Name}\a.txt");
            BackupObject backupObject2 = new BackupObject($@"{repository.Name}\{backupTask.Name}\b.txt");
            DirectoryInfo directoryInfo = new DirectoryInfo(Path.Combine(repository.FullPath, backupTask.Name));
            directoryInfo.Create();

            // Act.
            backupTask.AddBackupObject(backupObject1);
            backupTask.AddBackupObject(backupObject2);
            backupTask.CreateBackup();

            // Assert.
            Assert.Equal(directoryInfo.Name, backupTask.Name);
        }
    }
}