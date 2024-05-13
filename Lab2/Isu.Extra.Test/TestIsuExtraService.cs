using Isu.Extra.Entities;
using Isu.Extra.Models;
using Isu.Extra.Services;
using Isu.Models;
using Isu.Services;
using Xunit;
using DayOfWeek = Isu.Extra.Models.DayOfWeek;
using Stream = Isu.Extra.Entities.Stream;

namespace Isu.Extra.Test;

public class TestIsuExtraService
{
    private IsuExtraService _isu = new IsuExtraService();

    [Fact]
    public void AddOGNP_AddStudentToOGNP_StudentAmongTheEnrolledStudents_StudentDoesNotRegisteredToOGNP_GetStream()
    {
        Megafaculty megafaculty = _isu.AddMegafaculty("tint", new List<char>() { 'M', 'K', 'J' });
        OGNP ognp = _isu.AddOGNP("OGNP 1", "tint");
        Assert.Equal(ognp, megafaculty.Ognp);

        Lesson oopLesson1 = new Lesson(new Time(new TimeOnly(13, 30), DayOfWeek.Суббота, EvenOrOddWeek.Четная), new Classroom(2335), new Professor("Чикишев"));
        Lesson oopLesson2 = new Lesson(new Time(new TimeOnly(15, 20), DayOfWeek.Суббота, EvenOrOddWeek.Четная), new Classroom(2335), new Professor("Чикишев"));
        Timetable timetable = new Timetable(new List<Lesson>() { oopLesson1, oopLesson2 });
        GroupName groupName = new GroupName("M32061");
        GroupExtra groupExtra = _isu.AddGroupExtra(groupName, 'M');
        groupExtra.ChangeSchedule(timetable);
        Lesson ognpLesson1 = new Lesson(new Time(new TimeOnly(13, 30), DayOfWeek.Среда, EvenOrOddWeek.Четная), new Classroom(2426), new Professor("Зонис"));
        Lesson ognpLesson2 = new Lesson(new Time(new TimeOnly(15, 20), DayOfWeek.Среда, EvenOrOddWeek.Четная), new Classroom(2426), new Professor("Зонис"));
        Timetable ognpTimetable = new Timetable(new List<Lesson>() { ognpLesson1, ognpLesson2 });
        Stream stream = _isu.AddStream("OGNP 1", "1.1");
        GroupOGNP groupOgnp = _isu.AddGroupOgnp("OGNP 1", "1.1", "Зонис 1.1");
        groupOgnp.ChangeSchedule(ognpTimetable);
        StudentExtra studentExtra = _isu.AddStudentExtra("Петр Мошков", groupName);
        _isu.StudentRegistration(studentExtra, "OGNP 1", "Зонис 1.1");
        Assert.Contains(studentExtra, groupOgnp.Students);

        List<StudentExtra> registeredStudentsExtra = _isu.GetListOfStudentsInGroupOGNP("Зонис 1.1");
        Assert.Contains(studentExtra, registeredStudentsExtra);

        _isu.RemoveStudentRegistration(studentExtra, "Зонис 1.1");
        List<StudentExtra> unregisteredStudentsExtra = _isu.GetListOfUnregisteredStudentsInGroup(groupName);
        Assert.Contains(studentExtra, unregisteredStudentsExtra);

        List<Stream> streams = _isu.GetStream("OGNP 1");
        Assert.Equal(ognp.Streams, streams);
    }

    [Fact]
    public void TimetableOverlaps()
    {
        Megafaculty megafaculty = _isu.AddMegafaculty("tint", new List<char>() { 'M', 'K', 'J' });
        OGNP ognp = _isu.AddOGNP("OGNP 1", "tint");
        Assert.Equal(ognp, megafaculty.Ognp);

        Lesson oopLesson1 = new Lesson(new Time(new TimeOnly(13, 30), DayOfWeek.Суббота, EvenOrOddWeek.Четная), new Classroom(2335), new Professor("Чикишев"));
        Lesson oopLesson2 = new Lesson(new Time(new TimeOnly(15, 20), DayOfWeek.Суббота, EvenOrOddWeek.Четная), new Classroom(2335), new Professor("Чикишев"));
        Timetable timetable = new Timetable(new List<Lesson>() { oopLesson1, oopLesson2 });
        GroupName groupName = new GroupName("M32061");
        GroupExtra groupExtra = _isu.AddGroupExtra(groupName, 'M');
        groupExtra.ChangeSchedule(timetable);
        Lesson ognpLesson1 = new Lesson(new Time(new TimeOnly(13, 30), DayOfWeek.Суббота, EvenOrOddWeek.Четная), new Classroom(2426), new Professor("Зонис"));
        Lesson ognpLesson2 = new Lesson(new Time(new TimeOnly(15, 20), DayOfWeek.Суббота, EvenOrOddWeek.Четная), new Classroom(2426), new Professor("Зонис"));
        Timetable ognpTimetable = new Timetable(new List<Lesson>() { ognpLesson1, ognpLesson2 });
        Stream stream = _isu.AddStream("OGNP 1", "1.1");
        GroupOGNP groupOgnp = _isu.AddGroupOgnp("OGNP 1", "1.1", "Зонис 1.1");
        groupOgnp.ChangeSchedule(ognpTimetable);
        StudentExtra studentExtra = _isu.AddStudentExtra("Петр Мошков", groupName);
        Assert.ThrowsAny<IsuExtraException>(() =>
        {
            _isu.StudentRegistration(studentExtra, "OGNP 1", "Зонис 1.1");
        });
    }
}