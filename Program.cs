using Eisenhower_Matrix.Manager;
using Eisenhower_Matrix.Model;
using Eisenhower_Matrix.View;

namespace Eisenhower_Matrix
{

    internal class Program
    {
        public static string SelectedQuarter { get; private set; } = "IU";
        public static int SelectedTask { get; private set; } = 0;
        public static void Main(string[] args)
        {
           

            var manager = new MatrixDbManager();

            Console.WriteLine(manager.TestConnection());
            var input = new Input();
            var display = new Display();
            string currentOption = "T";

            bool isActive = true;
            while (isActive)
            {

                ToDoMatrix toDoMatrix = new ToDoMatrix();
                var userList = manager.GetAllItems();
                foreach (var item in userList)
                {
                    toDoMatrix.AddItem(item.Id, item.Title, item.Deadline, item.IsImportant, item.IsDone);
                }

                if (currentOption == "T")
                {
                    Console.Clear();
                    Console.WriteLine(toDoMatrix.ToString());
                    display.DisplayQuestion("Select an option:\n[A]dd task\n[D]elete selected task\n[S]elect task\n[Q]uit\nYour choice: ");
                    currentOption = Console.ReadLine().ToUpper();
                   
                    if (currentOption == "Q")
                    {
                        isActive = false;
                    }

                }
                else if (currentOption == "S")
                {
                    Console.Clear();
                    Console.WriteLine(toDoMatrix.ToString());
                    display.DisplayQuestion("Use up and down arrows to switch between tasks or use tab to switch between quarters.\nPress spacebar to mark task as done.\nPress enter to return to main menu.");
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.DownArrow)
                    {
                        SelectedTask++;
                    }
                    if (keyInfo.Key == ConsoleKey.UpArrow)
                    {
                        SelectedTask--;
                    }
                    if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        currentOption = "T";
                    }
                    if (keyInfo.Key == ConsoleKey.Tab)
                    {
                        SelectedTask = 0;
                        switch (SelectedQuarter)
                        {
                            case "IU":
                                SelectedQuarter = "IN";
                                break;
                            case "IN":
                                SelectedQuarter = "NU";
                                break;
                            case "NU":
                                SelectedQuarter = "NN";
                                break;
                            case "NN":
                                SelectedQuarter = "IU";
                                break;
                        }
                    }
                    if (keyInfo.Key == ConsoleKey.Spacebar)
                    {
                        var itemList = toDoMatrix.GetQuarter(SelectedQuarter);
                        var selectedItem = itemList.ToDoItems[SelectedTask];
                        if (SelectedQuarter == "IU" || SelectedQuarter == "IN")
                        {
                            selectedItem.MakeImportant();
                        }
                        if (selectedItem.IsDone == true)
                        {
                            selectedItem.Unmark();
                        }
                        else
                        {
                            selectedItem.Mark();
                        }
                        manager.UpdateItem(selectedItem);
                    }

                }
                else if (currentOption == "A")
                {
                    Console.Clear();
                    int id = 0;
                    display.DisplayQuestion("Input task title: ");
                    string userInputTitle = input.GetTitle();
                    display.DisplayQuestion("Input deadline in format DD-MM: ");
                    var deadline = input.GetDeadline();
                    display.DisplayQuestion("Is your task important? (Y/N)");
                    string importanceStatusInput = input.GetImportanceStatus();
                    bool isImportant = input.IsImportant(importanceStatusInput);
                    ToDoItem newItem = new(id, userInputTitle, deadline, false);
                    if (isImportant) newItem.MakeImportant();
                    manager.AddItem(newItem);
                    display.DisplayQuestion("Do you want to add next task? [Y/N]");
                    string? nextTask = Console.ReadLine();
                    if (nextTask == "Y")
                    {
                        currentOption = "A";
                    }
                    else
                    {
                        currentOption = "T";
                    }

                }
                else if (currentOption == "D")
                {
                    var itemList = toDoMatrix.GetQuarter(SelectedQuarter);
                    var selectedItem = itemList.ToDoItems[SelectedTask];
                    manager.DeleteItem(selectedItem);
                    currentOption = "T";
                }
            }
            Console.Clear();
            display.DisplayQuestion("Thank you for using our Application.\nSee you soon!\n\nCreated (2023) by:\nRadosław Rocławski\nGrzegorz Łabojko\nDaniel Czyż\n\n\n\n");
        }
    }
}