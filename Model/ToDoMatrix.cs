using System.Reflection.Metadata;
using System.Text;
using Eisenhower_Matrix.Model;

namespace Eisenhower_Matrix;

public class ToDoMatrix

{
    Dictionary<string, ToDoQuarter> ToDoQuarters = new Dictionary<string, ToDoQuarter>();
    //- 'IU' means that todoQuarter contains important todoItems & urgent
    //- 'IN' means that todoQuarter contains important todoItems & not urgent
    //- 'NU' means that todoQuarter contains not important todoItems & urgent
    //- 'NN' means that todoQuarter contains not important & not urgent todoItems

    public ToDoMatrix()
    {
        ToDoQuarters.Add("IU", new ToDoQuarter());
        ToDoQuarters.Add("IN", new ToDoQuarter());
        ToDoQuarters.Add("NU", new ToDoQuarter());
        ToDoQuarters.Add("NN", new ToDoQuarter());

    }

    public ToDoQuarter GetQuarter(string status)
    {
        if (ToDoQuarters.ContainsKey(status))
        {
            return ToDoQuarters[status];
        }
        else
        {
            throw new ArgumentException("Invalid status");
        }
    }
    
    public void AddItem(int id, string title, DateTime deadline, bool isImportant, bool isDone)
    {    
        string quarterKey = EstimateUrgency(deadline, isImportant);
        ToDoQuarters[quarterKey].AddItem(id, title, deadline, isDone);
    }

    public string EstimateUrgency(DateTime deadline, bool isImportant)
    {
        DateTime dateTime = DateTime.Now;
        TimeSpan dateDiff = deadline - dateTime;
        int daysDifference = (int)dateDiff.TotalDays;
        
        if (daysDifference <= 3 && isImportant == true)
        {
            return "IU";
        }
        else if (daysDifference > 3 && isImportant == true)
        { 
            return "IN"; 
        }
        else if (daysDifference <= 3 && isImportant == false)
        {
            return "NU";
        } 
        return "NN";
        
    }

    public void ArchiveItems()
    {
        // Removes all *TodoItem* objects with a parameter* isDone* set to *true* from list *todoItems*
        // in every element of dictionary *todoQuarters*
    }

    public override string ToString()
    {
        StringBuilder tableBuilder = new StringBuilder();
        int quarterIndex = 0;
        
        int rows = 4;
        int columns = 13;
        object[,] matrixTables = new object[rows, columns];

        string emptyString = new string(' ', 40);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                matrixTables[i, j] = emptyString;
            }
        }

        foreach (var quarter in ToDoQuarters)
        {
            ToDoQuarter toDoQuarter = quarter.Value;
            int itemIndex = 0;
            var insertLine = " ";
            
            foreach (ToDoItem item in toDoQuarter.GetItems())
            {
                string line;
    
                if (itemIndex == Program.SelectedTask && quarter.Key == Program.SelectedQuarter)
                {
                    line = $" {itemIndex + 1}. {item.ToString()}";
                    var lineLength = line.Length;
                    line = $"\u001b[31m{line}\u001b[0m"; // ANSI escape code for red text color
                    var spacesNeeded = 40 - lineLength;
                    insertLine = line + new string(' ', spacesNeeded);
                }
                else
                {
                    line = $" {itemIndex + 1}. {item.ToString()}";
                    var lineLength = line.Length;
                    var spacesNeeded = 40 - lineLength;
                    insertLine = line + new string(' ', spacesNeeded);
                }
                matrixTables[quarterIndex, itemIndex] = insertLine;
                itemIndex++;
            }
            quarterIndex++;
        }

        tableBuilder.AppendLine("    |                URGENT                  |               NOT URGENT               |");
        tableBuilder.AppendLine("  --|----------------------------------------|----------------------------------------|--");
        tableBuilder.AppendLine($"    |{matrixTables[0, 0]}|{matrixTables[1, 0]}|");
        tableBuilder.AppendLine($"    |{matrixTables[0, 1]}|{matrixTables[1, 1]}|");
        tableBuilder.AppendLine($"  I |{matrixTables[0, 2]}|{matrixTables[1, 2]}|");
        tableBuilder.AppendLine($"  M |{matrixTables[0, 3]}|{matrixTables[1, 3]}|");
        tableBuilder.AppendLine($"  P |{matrixTables[0, 4]}|{matrixTables[1, 4]}|");
        tableBuilder.AppendLine($"  O |{matrixTables[0, 5]}|{matrixTables[1, 5]}|");
        tableBuilder.AppendLine($"  R |{matrixTables[0, 6]}|{matrixTables[1, 6]}|");                                         
        tableBuilder.AppendLine($"  T |{matrixTables[0, 7]}|{matrixTables[1, 7]}|");
        tableBuilder.AppendLine($"  A |{matrixTables[0, 8]}|{matrixTables[1, 8]}|");
        tableBuilder.AppendLine($"  N |{matrixTables[0, 9]}|{matrixTables[1, 9]}|");
        tableBuilder.AppendLine($"  T |{matrixTables[0, 10]}|{matrixTables[1, 10]}|");
        tableBuilder.AppendLine($"    |{matrixTables[0, 11]}|{matrixTables[1, 11]}|");
        tableBuilder.AppendLine($"    |{matrixTables[0, 12]}|{matrixTables[1, 12]}|");
        tableBuilder.AppendLine("  --|----------------------------------------|----------------------------------------|--");
        tableBuilder.AppendLine($"  N |{matrixTables[2, 0]}|{matrixTables[3, 0]}|");
        tableBuilder.AppendLine($"  O |{matrixTables[2, 1]}|{matrixTables[3, 1]}|");
        tableBuilder.AppendLine($"  T |{matrixTables[2, 2]}|{matrixTables[3, 2]}|");
        tableBuilder.AppendLine($"    |{matrixTables[2, 3]}|{matrixTables[3, 3]}|");
        tableBuilder.AppendLine($"  I |{matrixTables[2, 4]}|{matrixTables[3, 4]}|");
        tableBuilder.AppendLine($"  M |{matrixTables[2, 5]}|{matrixTables[3, 5]}|");
        tableBuilder.AppendLine($"  P |{matrixTables[2, 6]}|{matrixTables[3, 6]}|");
        tableBuilder.AppendLine($"  O |{matrixTables[2, 7]}|{matrixTables[3, 7]}|");
        tableBuilder.AppendLine($"  R |{matrixTables[2, 8]}|{matrixTables[3, 8]}|");
        tableBuilder.AppendLine($"  T |{matrixTables[2, 9]}|{matrixTables[3, 9]}|");
        tableBuilder.AppendLine($"  A |{matrixTables[2, 10]}|{matrixTables[3, 10]}|");
        tableBuilder.AppendLine($"  N |{matrixTables[2, 11]}|{matrixTables[3, 11]}|");
        tableBuilder.AppendLine($"  T |{matrixTables[2, 12]}|{matrixTables[3, 12]}|");
        tableBuilder.AppendLine("  --|----------------------------------------|----------------------------------------|--");

        return tableBuilder.ToString();

    }
}