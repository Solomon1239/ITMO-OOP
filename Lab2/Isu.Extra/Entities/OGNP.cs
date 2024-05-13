using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class OGNP
{
    public OGNP(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new IsuExtraException("Stream's name is entered incorrectly");
        Name = name;
        Streams = new List<Stream>();
    }

    public OGNP()
    {
        Name = string.Empty;
        Streams = new List<Stream>();
    }

    public string Name { get; }
    public List<Stream> Streams { get; }

    public void AddStream(Stream stream)
    {
        if (Streams.FirstOrDefault(stream => stream.Name == Name) != null)
            throw new IsuExtraException("This stream is already exists");
        Streams.Add(stream);
    }
}