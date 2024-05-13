namespace Isu.Models;

public class GroupName
{
    private const int MinCourse = 1;
    private const int MaxMagistracyCourse = 2;
    private const int MaxBaccalaurCourse = 2;
    private const int NumOfBaccalaur = 3;
    private const int NumOfMagistracy = 4;
    private string _groupName;

    public GroupName(string groupName)
    {
        int qualification = int.Parse(groupName.Substring(1, 1));
        int course = int.Parse(groupName.Substring(2, 1));
        if (((qualification == NumOfBaccalaur) && (course is < MinCourse or > MaxBaccalaurCourse)) ||
            ((qualification == NumOfMagistracy) && (course is < MinCourse or > MaxMagistracyCourse)) ||
            (qualification is < NumOfBaccalaur or > NumOfMagistracy))
        {
            throw new IsuExceptions("Such group cannot exist");
        }

        _groupName = groupName;
        CourseNumber = new CourseNumber(_groupName[2] - '0');
    }

    public CourseNumber CourseNumber { get; }
}