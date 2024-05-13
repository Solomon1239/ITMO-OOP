namespace Isu.Models;

public class GenerateID
{
    public int StudentID { get; private set; } = 0;

    public int GeneraetID()
    {
        return ++StudentID;
    }
}