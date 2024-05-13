#nullable disable
using System.Collections.Generic;
using Backups.Entities;
using Backups.Models;

namespace Backups.Extra.Entities
{
    public class Restorer
    {
        private BackupTask _backupTask;
        private RestorePoint _restorePoint;
        private IRepository _repository;
        public Restorer(BackupTask backupTask, RestorePoint restorePoint, IRepository repository = null)
        {
            _backupTask = backupTask;
            _restorePoint = restorePoint;
            _repository = repository;
        }

        public void Restore()
        {
            IRepository repositoryToSave = _repository ?? _backupTask.Repository;

            BackupTask restoreBackupTask = new BackupTask(
                _backupTask.Name,
                _backupTask.StorageAlgorithm,
                repositoryToSave,
                _backupTask.Archiver);

            List<BackupObject> restorerBackupObjects = new List<BackupObject>();
            foreach (Storage storage in _restorePoint.Storages)
            {
                restorerBackupObjects.AddRange(storage.BackupObjects);
            }

            restorerBackupObjects.ForEach(restoreBackupTask.AddBackupObject);

            restoreBackupTask.CreateBackup();
        }
    }
}