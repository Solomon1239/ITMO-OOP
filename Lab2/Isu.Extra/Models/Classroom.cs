namespace Isu.Extra.Models;

public class Classroom
{
    public Classroom(int classroomNumber)
    {
        if (classroomNumber <= 0)
            throw new IsuExtraException("Classroom number cannot be less than 1");
        ClassroomNumber = classroomNumber;
    }

    public int ClassroomNumber { get; }
}