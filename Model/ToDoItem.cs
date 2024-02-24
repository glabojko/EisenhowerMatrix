namespace Eisenhower_Matrix.Model;

public class ToDoItem
{
    public string Title { get; set; }
    public DateTime Deadline { get; set; }
    public bool IsImportant { get; set; } = false;
    public bool IsDone { get; set; } = false;
    public int Id { get; set; }
    public char ItemMark { get; set; }
    

    public ToDoItem(int id, string title, DateTime deadline, bool isDone)
    {
        if (deadline < DateTime.UtcNow)
        {
            throw new ArgumentException("Deadline cannot be in the past", nameof(deadline));
        }

        Id = id;
        Title = title;
        Deadline = deadline;
        IsDone = isDone;
    }

    public string CreateToDoItem(string title, DateTime deadline)
    {
        Title = title;
        Deadline = deadline;
        return ToString();
    }

    public void MakeImportant()
    {
        IsImportant = true;
    }
    public void Mark()
    {
        IsDone = true;
    }

    public void Unmark()
    {
        IsDone = false;
    }

    public override string ToString()
    {
        ItemMark = IsDone ? 'X' : ' ';
        return $"[{ItemMark}] {Deadline.Day}-{Deadline.Month} {Title}";
    }


}