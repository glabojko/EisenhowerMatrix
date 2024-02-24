using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eisenhower_Matrix.Model
{
    [TestFixture]
    public class ToDoItemTests
    {
        [Test]
        public void CreateToDoItem_PastDeadline_ShouldThrowException()
        {
            int id = 1;
            string title = "Task with Past Deadline";
            DateTime pastDeadline = DateTime.Now.AddDays(-1); // Setting a past deadline
            bool isDone = false;

            Assert.Throws<ArgumentException>(() => new ToDoItem(id, title, pastDeadline, isDone));
        }
    }
}
