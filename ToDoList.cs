using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDo
{
    public class ToDoList
    {
        public ToDoList(string title, string project, DateTime dueDate, string status)
        {
            Title = title;
            Project = project;
            DueDate = dueDate;
            Status = status;
        }
        public string Title {get; set;}
        public string Project {get; set;}
        public DateTime DueDate {get; set;}
        public string Status {get; set;}
    }

}