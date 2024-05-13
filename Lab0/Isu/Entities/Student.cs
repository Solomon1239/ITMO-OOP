using Isu.Models;

namespace Isu.Entities;

public class Student
{
    private string _name;

    public Student(string name, int id, GroupName groupName)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new IsuExceptions("Incorrect student's name");
        }

        _name = name;
        Id = id;
        GroupName = groupName ?? throw new ArgumentNullException(nameof(groupName));
    }

    public int Id { get; }
    public GroupName GroupName { get; private set; }

    public void ChangeGroup(Group newGroup)
    {
        GroupName = newGroup.GroupName;
    }
}