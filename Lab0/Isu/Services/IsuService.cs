using Isu.Entities;
using Isu.Models;

namespace Isu.Services;

public class IsuService : IIsuService
{
    private List<Group> _groups = new List<Group>();
    private GenerateID generateID = new GenerateID();
    public Group AddGroup(GroupName name)
    {
        var newGroup = new Group(name);
        if (FindGroup(name) != null)
        {
            throw new IsuExceptions("Group is already exists");
        }

        _groups.Add(newGroup);

        return newGroup;
    }

    public Student AddStudent(Group group, string name)
    {
        if (_groups.First(curGroup => curGroup.GroupName == group.GroupName) is null)
            throw new IsuExceptions("No such group exists");
        var newStudent = new Student(name, generateID.GeneraetID(), group.GroupName);
        _groups.First(curGroup => curGroup.GroupName == group.GroupName).AddStudent(newStudent);
        return newStudent;
    }

    public Student GetStudent(int id)
    {
        Student? curStudent = FindStudent(id);
        if (curStudent == null)
        {
            throw new IsuExceptions("No such student exists");
        }

        return curStudent;
    }

    public Student? FindStudent(int id)
    {
        return _groups.SelectMany(curGroup => curGroup.Students).FirstOrDefault(curStudent => curStudent.Id == id);
    }

    public List<Student> FindStudents(GroupName groupName)
    {
        Group? curGroup = FindGroup(groupName);
        return curGroup?.Students.ToList() ?? new List<Student>();
    }

    public List<Student> FindStudents(CourseNumber courseNumber)
    {
        List<Student> curStudents;
        curStudents = _groups.FindAll(group => group.GroupName.CourseNumber == courseNumber)
            .SelectMany(group => group.Students).ToList();

        return curStudents;
    }

    public Group? FindGroup(GroupName groupName)
    {
        return _groups.FirstOrDefault(group => group.GroupName == groupName);
    }

    public List<Group> FindGroups(CourseNumber courseNumber)
    {
        return _groups.FindAll(group => group.GroupName.CourseNumber == courseNumber);
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        if ((FindStudent(student.Id) == null) || (FindGroup(newGroup.GroupName) == null))
        {
            throw new IsuExceptions("No such group or student exists");
        }

        _groups.First(group => group.GroupName == student.GroupName).RemoveStudent(student);
        _groups.First(group => group == newGroup).AddStudent(student);
        student.ChangeGroup(newGroup);
    }
}