using System;
using System.Collections.Generic;
using Backups.Algorithms;
using Backups.Tools;

namespace Backups.Models
{
    public class RestorePoint
    {
        private List<Storage> _storages;
        private int _archiveNumber;

        public RestorePoint(DateTime timeOfCreation, List<Storage> storages, int archiveNumber)
        {
            if (storages.Count == 0) throw new BackupsException("Nothing to save");
            _storages = storages;

            _archiveNumber = archiveNumber;

            Date = timeOfCreation;
            Id = Guid.NewGuid();
        }

        public DateTime Date { get; private set; }
        public Guid Id { get; }

        public IReadOnlyCollection<Storage> Storages => _storages.AsReadOnly();

        public void AddStorage(Storage storage)
        {
            if (_storages.Contains(storage)) throw new BackupsException("Storage already exist");

            _storages.Add(storage);
        }

        public void RemoveStorage(Storage storage)
        {
            if (!_storages.Contains(storage)) throw new BackupsException("Storage does not exist");

            _storages.Remove(storage);
        }

        public void SetCreationDate(DateTime date)
        {
            Date = date;
        }
    }
}