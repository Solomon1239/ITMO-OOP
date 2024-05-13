namespace Backups.Entities
{
    public interface IArchiver
    {
        void Archive(string originalPath, string compressedPath);
    }
}