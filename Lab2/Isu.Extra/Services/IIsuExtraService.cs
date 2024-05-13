using Isu.Extra.Entities;
using Isu.Models;
using Stream = Isu.Extra.Entities.Stream;

namespace Isu.Extra.Services;

public interface IIsuExtraService
{
    StudentExtra AddStudentExtra(string name, GroupName groupName);
    GroupExtra AddGroupExtra(GroupName groupName, char megafacultyLetter);
    OGNP AddOGNP(string name, string megafacultyName);
    List<Stream> GetStream(string ognpName);
    List<StudentExtra> GetListOfStudentsInGroupOGNP(string groupOGNPName);
    List<StudentExtra> GetListOfUnregisteredStudentsInGroup(GroupName groupName);
    void StudentRegistration(StudentExtra student, string ognpName, string ognpGroupName);
    void RemoveStudentRegistration(StudentExtra student, string ognpGroupName);
}