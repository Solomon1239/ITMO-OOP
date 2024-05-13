using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Models;
using Isu.Models;
using Isu.Services;
using Stream = Isu.Extra.Entities.Stream;

namespace Isu.Extra.Services;

public class IsuExtraService : IIsuService, IIsuExtraService
{
    private readonly IsuService _decorate;
    private List<Megafaculty> _megafaculties = new List<Megafaculty>();
    private GenerateID generateID = new GenerateID();

    public IsuExtraService()
    {
        _decorate = new IsuService();
    }

    public Megafaculty AddMegafaculty(string name, List<char> letters)
    {
        if (_megafaculties.FirstOrDefault(megafaculty => megafaculty.Name == name) != null)
            throw new IsuExtraException("Such megafaculty already exists");
        Megafaculty newMegafaculty = new Megafaculty(name, letters);
        _megafaculties.Add(newMegafaculty);
        return newMegafaculty;
    }

    public StudentExtra AddStudentExtra(string name, GroupName groupName)
    {
        if (_megafaculties.Count == 0)
            throw new IsuExtraException("Please create a megafaculty first");
        if (string.IsNullOrWhiteSpace(name))
            throw new IsuExtraException("Name entered incorrectly");
        var allGroups = _megafaculties.SelectMany(megafaculty =>
            megafaculty.Groups);
        GroupExtra? neededGroup = allGroups.FirstOrDefault(group => group.GroupName == groupName);
        if (neededGroup is null)
            throw new IsuExtraException("No such group exists");
        StudentExtra student = new StudentExtra(name, generateID.GeneraetID(), groupName);
        neededGroup.AddStudentExtra(student);
        return student;
    }

    public GroupExtra AddGroupExtra(GroupName groupName, char megafacultyLetter)
    {
        if (_megafaculties.Count == 0)
            throw new IsuExtraException("Please create a megafaculty first");
        GroupExtra newGroup = new GroupExtra(groupName, megafacultyLetter);
        Megafaculty? megafaculty = _megafaculties.FirstOrDefault(megafaculty =>
            megafaculty.Letters.Any(letter => letter == megafacultyLetter));
        if (megafaculty is null)
            throw new IsuExtraException("There is no megafaculty for which this group is suitable");
        if (megafaculty.Groups.FirstOrDefault(group => group == newGroup) != null)
            throw new IsuExtraException("Such group already exists");
        megafaculty.AddGroup(newGroup);
        return newGroup;
    }

    public OGNP AddOGNP(string name, string megafacultyName)
    {
        if (_megafaculties.Count == 0)
            throw new IsuExtraException("Please create a megafaculty first");
        if (string.IsNullOrWhiteSpace(name))
            throw new IsuExtraException("Name entered incorrectly");
        Megafaculty? megafaculty = _megafaculties.FirstOrDefault(megafaculty => megafaculty.Name == megafacultyName);
        if (megafaculty is null)
            throw new IsuExtraException("Such megafaculty does not exist");
        OGNP newOGNP = new OGNP(name);
        megafaculty.AddOGNP(newOGNP);
        return newOGNP;
    }

    public Stream AddStream(string ognpName, string streamName)
    {
        OGNP? ognp = _megafaculties.Select(megafaculty => megafaculty.Ognp)
            .FirstOrDefault(ognp => ognp.Name == ognpName);
        if (ognp is null)
            throw new IsuExtraException("Such OGNP does not exist");
        Stream? stream = ognp.Streams.FirstOrDefault(stream => stream.Name == streamName);
        if (stream != null)
            throw new IsuExtraException("Such stream does not exist");
        Stream newStream = new Stream(streamName);
        ognp.AddStream(new Stream(streamName));
        return newStream;
    }

    public GroupOGNP AddGroupOgnp(string ognpName, string streamName, string ognpGroupName)
    {
        OGNP? ognp = _megafaculties.Select(megafaculty => megafaculty.Ognp)
            .FirstOrDefault(ognp => ognp.Name == ognpName);
        if (ognp is null)
            throw new IsuExtraException("Such OGNP does not exist");
        Stream? stream = ognp.Streams.FirstOrDefault(stream => stream.Name == streamName);
        if (stream is null)
            throw new IsuExtraException("Such stream does not exist");
        GroupOGNP newGroup = new GroupOGNP(ognpGroupName);
        stream.AddGroupOGNP(newGroup);
        return newGroup;
    }

    public List<Stream> GetStream(string ognpName)
    {
        if (string.IsNullOrWhiteSpace(ognpName))
            throw new IsuExtraException("Name entered incorrectly");
        Megafaculty? neededMegafaculty = _megafaculties.FirstOrDefault(megafaculty => megafaculty.Ognp.Name == ognpName);
        if (neededMegafaculty is null)
            throw new IsuExtraException("Such OGNP does not exist");
        return neededMegafaculty.Ognp.Streams;
    }

    public List<StudentExtra> GetListOfStudentsInGroupOGNP(string groupOGNPName)
    {
        if (string.IsNullOrWhiteSpace(groupOGNPName))
            throw new IsuExtraException("Name entered incorrectly");
        var groupList = _megafaculties.SelectMany(megafaculty =>
            megafaculty.Ognp.Streams).SelectMany(stream => stream.Groups);
        GroupOGNP? neededGroupOGNP = groupList.FirstOrDefault(group => group.Name == groupOGNPName);
        if (neededGroupOGNP is null)
            throw new IsuExtraException("Such GroupOGNP does not exist");
        return neededGroupOGNP.Students;
    }

    public List<StudentExtra> GetListOfUnregisteredStudentsInGroup(GroupName groupName)
    {
        GroupExtra? neededGroup = _megafaculties.SelectMany(megafaculty =>
            megafaculty.Groups).FirstOrDefault(group => group.GroupName == groupName);
        if (neededGroup is null)
            throw new IsuExtraException("No such group exists");
        return neededGroup.StudentsExtra.Where(student => student.RegistrationForOGNP.Count == 0).ToList();
    }

    public void StudentRegistration(StudentExtra student, string ognpName, string ognpGroupName)
    {
        const int maxNumberOfStudents = 30;
        const int maxOgnp = 2;
        if (student.RegistrationForOGNP.Count >= maxOgnp)
            throw new IsuExtraException("Student is enrolled in the maximum number of OGNP");
        Megafaculty? megafaculty = _megafaculties.FirstOrDefault(megafaculty => megafaculty.Ognp.Name == ognpName);
        if (megafaculty is null)
            throw new IsuExtraException("Such OGNP doesn't exist");
        GroupOGNP? groupOgnp = megafaculty.Ognp.Streams.SelectMany(stream => stream.Groups)
            .FirstOrDefault(group => group.Name == ognpGroupName);
        if (groupOgnp is null)
            throw new IsuExtraException("Such OGNP group doesn't exist");
        if (groupOgnp.NumberOfStudents == maxNumberOfStudents)
            throw new IsuExtraException("The group is full");
        GroupExtra? groupExtra = _megafaculties.SelectMany(megafaculty =>
            megafaculty.Groups).FirstOrDefault(group => group.GroupName == student.GroupName);
        if (groupExtra is null)
            throw new IsuExtraException("Such groupdoesn't exist");
        foreach (var lesson in groupExtra.GroupTimetable.LessonsList)
            {
                if (groupOgnp.GroupOgnpTimetable.IntersectionOfLessons(lesson) is false)
                    throw new IsuExtraException("OGNP timetable overlaps with the main timetable");
            }

        student.RegistrationForOGNP.Add(groupOgnp);
        groupOgnp.Students.Add(student);
    }

    public void RemoveStudentRegistration(StudentExtra student, string ognpGroupName)
    {
        if (student.RegistrationForOGNP.Count == 0)
            throw new IsuExtraException("Student is not registered for any OGNP");
        GroupOGNP? neededGroup = _megafaculties.SelectMany(megafaculty => megafaculty.Ognp.Streams)
            .SelectMany(stream => stream.Groups)
            .FirstOrDefault(group => group.Students.Contains(student));
        if (neededGroup is null)
            throw new IsuExtraException("Such OGNP group doesn't exist");
        student.RegistrationForOGNP.Remove(neededGroup);
        neededGroup.Students.Remove(student);
    }

    public Group AddGroup(GroupName name)
    {
        return _decorate.AddGroup(name);
    }

    public Student AddStudent(Group group, string name)
    {
        return _decorate.AddStudent(group, name);
    }

    public Student GetStudent(int id)
    {
        return _decorate.GetStudent(id);
    }

    public Student? FindStudent(int id)
    {
        return _decorate.FindStudent(id);
    }

    public List<Student> FindStudents(GroupName groupName)
    {
        return _decorate.FindStudents(groupName);
    }

    public List<Student> FindStudents(CourseNumber courseNumber)
    {
        return _decorate.FindStudents(courseNumber);
    }

    public Group? FindGroup(GroupName groupName)
    {
        return _decorate.FindGroup(groupName);
    }

    public List<Group> FindGroups(CourseNumber courseNumber)
    {
        return _decorate.FindGroups(courseNumber);
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        _decorate.ChangeStudentGroup(student, newGroup);
    }
}