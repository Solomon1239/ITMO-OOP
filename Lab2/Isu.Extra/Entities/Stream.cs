using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class Stream
{
    public Stream(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new IsuExtraException("Stream's name is entered incorrectly");
        Name = name;
        Groups = new List<GroupOGNP>();
    }

    public string Name { get; }
    public List<GroupOGNP> Groups { get; }

    public void AddGroupOGNP(GroupOGNP groupOgnp)
    {
        if (Groups.FirstOrDefault(group => group.Name == Name) != null)
            throw new IsuExtraException("This group is already exists");
        Groups.Add(groupOgnp);
    }
}