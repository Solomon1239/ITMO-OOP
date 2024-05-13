using Isu.Entities;
using Isu.Extra.Models;
using Isu.Models;

namespace Isu.Extra.Entities;

public class GroupOGNP
{
    public GroupOGNP(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new IsuExtraException("GroupOGNP's name is entered incorrectly");
        Name = name;
        GroupOgnpTimetable = new Timetable(new List<Lesson>());
        Students = new List<StudentExtra>();
        NumberOfStudents = Students.Count;
    }

    public string Name { get; }
    public Timetable GroupOgnpTimetable { get; private set; }
    public List<StudentExtra> Students { get; }
    public int NumberOfStudents { get; }
    public void ChangeSchedule(Timetable newGroupTimetable)
    {
        GroupOgnpTimetable = newGroupTimetable;
    }
}