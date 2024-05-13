namespace Backups.Models
{
    public class ArchiveNumber
    {
        private int _archiveNumber = 0;

        public int GenerateNumber()
        {
            return ++_archiveNumber;
        }
    }
}