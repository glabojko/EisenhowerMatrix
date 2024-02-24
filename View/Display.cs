namespace Eisenhower_Matrix.View;

internal class Display
{
    public void DisplayQuestion(string givenQuestion)
    {
        Console.Write(givenQuestion);
    }

    public void DisplayStatus(int importanceStatus)
    {
        Console.WriteLine(importanceStatus);
        
    }
}
