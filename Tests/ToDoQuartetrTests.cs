using NUnit.Framework;
using System;

namespace Eisenhower_Matrix.Tests
{
    [TestFixture]
    public class ToDoQuarterTests
    {
        [Test]
        public void AddItem_PastDeadline_ShouldThrowArgumentException()
        {
            
            var toDoQuarter = new ToDoQuarter();
            int id = 1;
            string title = "Task with Past Deadline";
            DateTime pastDeadline = DateTime.UtcNow.AddDays(-1); 
            bool isDone = false;

     
            Assert.Throws<ArgumentException>(() => toDoQuarter.AddItem(id, title, pastDeadline, isDone));
        }

        [Test]
        public void RemoveItem_InvalidIndex_ShouldThrowArgumentOutOfRangeException()
        {  
            var toDoQuarter = new ToDoQuarter();
            int invalidIndex = -1;

            Assert.Throws<ArgumentOutOfRangeException>(() => toDoQuarter.RemoveItem(invalidIndex));
        }
    }
}
