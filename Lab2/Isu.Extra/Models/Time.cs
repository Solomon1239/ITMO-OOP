namespace Isu.Extra.Models;

public enum DayOfWeek
{
    Понедельник,
    Вторник,
    Среда,
    Четверг,
    Пятница,
    Суббота,
    Воскресенье,
}

public enum EvenOrOddWeek
{
    Четная,
    Нечетная,
}

public class Time
{
    public Time(TimeOnly time, DayOfWeek day, EvenOrOddWeek week)
    {
        LessonTime = time;
        Day = day;
        Week = week;
    }

    public TimeOnly LessonTime { get; }
    public DayOfWeek Day { get; }
    public EvenOrOddWeek Week { get; }
}