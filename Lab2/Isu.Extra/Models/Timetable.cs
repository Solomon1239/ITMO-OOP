namespace Isu.Extra.Models;

public class Timetable
{
    public Timetable(List<Lesson> newTimetable)
    {
        LessonsList = newTimetable;
    }

    public List<Lesson> LessonsList { get; }

    public bool IntersectionOfLessons(Lesson newLesson)
    {
        const int minHoursDiffBetweenLessons = 0;
        const int maxHoursDiffBetweenLessons = 1;
        const int maxMinutesDiffBetweenLessons = 40;
        foreach (var lesson in LessonsList)
        {
            if (lesson.LessonStartTime.Day == newLesson.LessonStartTime.Day && lesson.LessonStartTime.Week == newLesson.LessonStartTime.Week)
            {
                if (Math.Abs(lesson.LessonStartTime.LessonTime.Hour - newLesson.LessonStartTime.LessonTime.Hour) ==
                    minHoursDiffBetweenLessons ||
                    (Math.Abs(lesson.LessonStartTime.LessonTime.Hour - newLesson.LessonStartTime.LessonTime.Hour) ==
                     maxHoursDiffBetweenLessons &&
                     Math.Abs(lesson.LessonStartTime.LessonTime.Minute - newLesson.LessonStartTime.LessonTime.Minute) <
                     maxMinutesDiffBetweenLessons))
                return false;
            }
        }

        return true;
    }

    public void AddLesson(Lesson newLesson)
    {
        if (IntersectionOfLessons(newLesson) is false)
            throw new IsuExtraException("Impossible to add a lesson, because it overlaps with the main timetable");
        LessonsList.Add(newLesson);
    }
}