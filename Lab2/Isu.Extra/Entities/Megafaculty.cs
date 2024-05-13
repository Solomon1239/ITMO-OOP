using Isu.Extra.Models;

namespace Isu.Extra.Entities;

public class Megafaculty
{
    public Megafaculty(string name, List<char> letters)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new IsuExtraException("Megafaculty's name is entered incorrectly");
        if (letters.Count == 0)
            throw new IsuExtraException("List of letters is empty");
        Name = name;
        Letters = letters;
        Ognp = new OGNP();
        Groups = new List<GroupExtra>();
    }

    public string Name { get; }
    public List<char> Letters { get; }
    public OGNP Ognp { get; private set; }
    public List<GroupExtra> Groups { get; }

    public void AddGroup(GroupExtra newGroup)
    {
        if (Letters.FirstOrDefault(letter => letter == newGroup.MegafacultyLetter) is '\0')
            throw new IsuExtraException("The group does not belong to this megafaculty");
        Groups.Add(newGroup);
    }

    public void AddOGNP(OGNP ognp)
    {
        if (Ognp.Name != string.Empty)
            throw new IsuExtraException("This megafaculty already has OGNP");
        Ognp = ognp;
    }
}