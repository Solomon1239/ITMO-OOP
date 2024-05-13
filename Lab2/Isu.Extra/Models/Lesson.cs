using Isu.Extra.Entities;

namespace Isu.Extra.Models;

public class Lesson
{
    public Lesson(Time lessonStartTime, Classroom classroom, Professor professor)
    {
        LessonStartTime = lessonStartTime;
        Classroom = classroom;
        Professor = professor;
    }

    public Time LessonStartTime { get; }
    public Classroom Classroom { get; }
    public Professor Professor { get; }
}