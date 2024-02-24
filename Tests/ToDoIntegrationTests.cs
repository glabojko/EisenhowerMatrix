using NUnit.Framework;
using System;

namespace Eisenhower_Matrix.Tests
{
    [TestFixture]
    public class ToDoIntegrationTests
    {
        [Test]
        public void AddItem_UpdatesQuarterListCorrectly()
        {
            var toDoQuarter = new ToDoQuarter();
            int id = 1;
            string title = "New Task";
            DateTime deadline = DateTime.UtcNow.AddDays(1); 
            bool isDone = false;

            toDoQuarter.AddItem(id, title, deadline, isDone);

            var items = toDoQuarter.GetItems();
            Assert.AreEqual(1, items.Count, "Item count is incorrect");

            var addedItem = items[0];
            Assert.AreEqual(id, addedItem.Id, "Item Id is incorrect");
            Assert.AreEqual(title, addedItem.Title, "Item Title is incorrect");
            Assert.AreEqual(deadline, addedItem.Deadline, "Item Deadline is incorrect");
            Assert.AreEqual(isDone, addedItem.IsDone, "Item IsDone status is incorrect");
        }
    }
}

