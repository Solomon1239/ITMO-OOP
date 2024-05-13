using System.IO;
using System.IO.Compression;

namespace Backups.Entities
{
    public class ArchiverGz : IArchiver
    {
        public void Archive(string originalPath, string compressedPath)
        {
            using FileStream originalFileStream = new FileStream(originalPath, FileMode.OpenOrCreate);
            using FileStream compressedFileStream = File.Create($@"{compressedPath}.gz");
            using GZipStream compressor = new GZipStream(compressedFileStream, CompressionMode.Compress);
            originalFileStream.CopyTo(compressor);
        }
    }
}