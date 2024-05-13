namespace Isu.Models;

public class CourseNumber
{
    private const int MinCourse = 1;
    private const int MaxCourse = 4;
    public CourseNumber(int courseNumber)
    {
        if (courseNumber is < MinCourse or > MaxCourse)
        {
            throw new IsuExceptions("Course cannot be less than 1 or more than 4");
        }

        Number = courseNumber;
    }

    public int Number { get; }
}