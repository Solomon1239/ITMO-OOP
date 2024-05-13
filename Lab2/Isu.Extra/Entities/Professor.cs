using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class Professor
{
    public Professor(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new IsuExtraException("Professor's name is entered incorrectly");
        Name = name;
    }

    public string Name { get; }
}