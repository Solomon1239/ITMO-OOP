using Isu.Entities;
using Isu.Models;
using Isu.Services;
using Xunit;

namespace Isu.Test;

public class TestIsuService
{
    private IsuService _isu = new IsuService();

    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
    {
        GroupName groupName = new GroupName("M32061");
        Group newGroup = _isu.AddGroup(groupName);
        Student newStudent = _isu.AddStudent(newGroup, "Moshkov Petr");
        Assert.Contains(newStudent, newGroup.Students);
    }

    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException()
    {
        int maxGroupSize = 30;
        GroupName groupName = new GroupName("M32061");
        Group newGroup = _isu.AddGroup(groupName);
        for (int i = 0; i < maxGroupSize; i++)
        {
            Student newStudent = _isu.AddStudent(newGroup, "Moshkov Petr");
        }

        Assert.ThrowsAny<Exception>(() =>
        {
            Student newStudent = _isu.AddStudent(newGroup, "Moshkov Petr");
        });
    }

    [Fact]
    public void CreateGroupWithInvalidName_ThrowException()
    {
        Assert.ThrowsAny<Exception>(() =>
        {
            GroupName groupName = new GroupName("M37061");
            Group group = new Group(groupName);
        });
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        Group group = _isu.AddGroup(new GroupName("M32061"));
        Student student = _isu.AddStudent(group, "Moshkov Petr");
        Group newGroup = _isu.AddGroup(new GroupName("M32051"));
        _isu.ChangeStudentGroup(student, newGroup);
        Assert.Equal(student.GroupName, newGroup.GroupName);
    }
}