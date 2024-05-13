using Isu.Entities;
using Isu.Extra.Models;
using Isu.Models;

namespace Isu.Extra.Entities;

public class GroupExtra : Group
{
    public GroupExtra(GroupName groupName, char megafacultyLetter)
        : base(groupName)
    {
        if (!char.IsLetter(megafacultyLetter))
            throw new IsuExtraException("Incorrect megafaculty letter entered");
        GroupTimetable = new Timetable(new List<Lesson>());
        MegafacultyLetter = megafacultyLetter;
        StudentsExtra = new List<StudentExtra>();
    }

    public Timetable GroupTimetable { get; private set; }
    public char MegafacultyLetter { get; }
    public List<StudentExtra> StudentsExtra { get; }

    public void ChangeSchedule(Timetable newGroupTimetable)
    {
        GroupTimetable = newGroupTimetable;
    }

    public void AddLessonInSchedule(Lesson newLesson)
    {
        GroupTimetable.AddLesson(newLesson);
    }

    public void AddStudentExtra(StudentExtra studentExtra)
    {
        if (StudentsExtra.FirstOrDefault(student => student.Id == studentExtra.Id) != null)
            throw new IsuExceptions("This student is already in the group");

        StudentsExtra.Add(studentExtra);
    }
}