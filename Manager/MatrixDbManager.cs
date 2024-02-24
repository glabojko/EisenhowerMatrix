using Eisenhower_Matrix.Model;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace Eisenhower_Matrix.Manager;

public class MatrixDbManager
{
   
    private string? connectionString => ConfigurationManager.AppSettings["connectionString"];
    private IToDoItemDao toDoItemDao;

    public MatrixDbManager()
    {
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("Connection string is missing or empty.");
        }

        toDoItemDao = new ToDoItemDao(connectionString);
    }

    public void AddItem(ToDoItem toDoItem)
    {
        toDoItemDao.Add(toDoItem);
    }

    public void DeleteItem(ToDoItem toDoItem)
    {
        toDoItemDao.Delete(toDoItem);
    }

    public void UpdateItem(ToDoItem toDoItem) => toDoItemDao.Update(toDoItem);

    public List<ToDoItem> GetAllItems() => toDoItemDao.GetAll();

    public bool TestConnection()
    {
        using var connection = new SqlConnection(connectionString);

        try
        {
            connection.Open();
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }
}
