using Isu.Entities;
using Isu.Models;

namespace Isu.Extra.Entities;

public class StudentExtra : Student
{
    public StudentExtra(string name, int id, GroupName groupName)
        : base(name, id, groupName)
    {
        RegistrationForOGNP = new List<GroupOGNP>();
    }

    public List<GroupOGNP> RegistrationForOGNP { get; }
}