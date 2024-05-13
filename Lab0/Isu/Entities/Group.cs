using System.Collections.ObjectModel;
using Isu.Models;
using Microsoft.VisualBasic;

namespace Isu.Entities;

public class Group
{
    private const int MaxGroupSize = 30;
    private List<Student> _students;
    public Group(GroupName groupName)
    {
        GroupName = groupName;
        _students = new List<Student>();
    }

    public IReadOnlyCollection<Student> Students => _students.AsReadOnly();
    public GroupName GroupName { get; }

    public void AddStudent(Student newStudent)
    {
        if (_students.FirstOrDefault(student => student.Id == newStudent.Id) != null)
            throw new IsuExceptions("This student is already in the group");
        if (Students.Count >= MaxGroupSize)
        {
            throw new IsuExceptions("Group is full");
        }

        _students.Add(newStudent);
    }

    public void RemoveStudent(Student student)
    {
        if (!_students.Remove(student))
            throw new IsuExceptions("No such student exists");
    }
}