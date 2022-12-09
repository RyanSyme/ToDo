using System.Linq;
using System.Text.Json;
using ToDo;

// Loads the current ToDo list from a json file
string toDoJsonData = File.ReadAllText("ToDo.json");
var myToDo = JsonSerializer.Deserialize<List<ToDoList>>(toDoJsonData);

bool RunProgram = true;
// Loops through program
while (RunProgram)
{
    Console.WriteLine("Please choose an option from the list:");
    Console.WriteLine("");
    Console.WriteLine("[1]: *DISPLAY* your current ToDo list");
    Console.WriteLine("[2]: *ADD* a new task to your ToDo list");
    Console.WriteLine("[3]: *EDIT/UPDATE* a previously entered task");
    Console.WriteLine("[4]: *DELETE* a task");
    Console.WriteLine("[5]: *SAVE* and *EXIT* application");
    Console.WriteLine("---------------------------------------");
    Console.Write("Please enter an option: ");

    string UserChoice = (Console.ReadLine() ?? "");
    if (UserChoice == "1")
    {
        Console.WriteLine("");
        Console.WriteLine("Would you like to see the tasks ordered by: ");
        Console.WriteLine("[1]: Project");
        Console.WriteLine("[2]: Due Date");
        Console.WriteLine("");

        string orderChoice = (Console.ReadLine() ?? "");
        bool choiceMade = false;
        // Displays new option to see list ordered by project or by due date
        while (!choiceMade)
        {
            if (orderChoice == "1")
            {
                List<ToDoList> currentToDo = myToDo!.OrderBy(x => x.Project).ToList();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"| {"Title".PadRight(24)} {"Project".PadRight(16)} {"DueDate".PadRight(12)} | {"Status"}");
                Console.WriteLine("---------------------------------------------------------");
                foreach (var task in currentToDo)
                {
                    Console.WriteLine($"| {task.Title.PadRight(24)} {task.Project.PadRight(16)} {task.DueDate.ToString("yyyy-MM-dd").PadRight(12)} | {task.Status}");
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("---------------------------------------");
                Console.WriteLine();
                choiceMade = true;
            }
            else if (orderChoice == "2")
            {
                List<ToDoList> byDateToDo = myToDo!.OrderBy(x => x.DueDate).ToList();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"| {"Title".PadRight(24)} {"Project".PadRight(16)} {"DueDate".PadRight(12)} | {"Status"}");
                Console.WriteLine("---------------------------------------------------------");
                foreach (var task in byDateToDo)
                {
                    Console.WriteLine($"| {task.Title.PadRight(24)} {task.Project.PadRight(16)} {task.DueDate.ToString("yyyy-MM-dd").PadRight(12)} | {task.Status}");
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("---------------------------------------");
                Console.WriteLine();
                choiceMade = true;
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Please enter a valid number: ");
                Console.WriteLine();
            }
        }
    }
    //  Starts function to add a new item to the ToDo list
    else if (UserChoice == "2")
    {
        Console.WriteLine("Please add a new task to the ToDo list:");
        Console.WriteLine("");
        Console.WriteLine("Enter the task title:");
        string title = (Console.ReadLine() ?? "");
        Console.WriteLine("Enter the task's project name:");
        string project = (Console.ReadLine() ?? "");
        Console.WriteLine("Enter the task's due date (YYYY-MM-DD):");
        bool isCorrect = false;
        string userTime = "";
        DateTime dueDate = new DateTime();
        while (!isCorrect)
        {
            userTime = (Console.ReadLine() ?? "").Trim().ToLower();
            if (DateTime.TryParse(userTime, out dueDate))
            {
                isCorrect = true;
            }
            else
            {
                Console.WriteLine("Invalid Date");
            }
        }
        Console.WriteLine("Has the task been completed Y/n?:");
        string userEntry = (Console.ReadLine() ?? "").Trim().ToLower(); ;
        string status = "";
        if (userEntry == "y")
        {
            status = "Complete";
        }
        else
        {
            status = "";
        }
        var newTask = new ToDoList(title, project, dueDate, status);
        myToDo?.Add(newTask);
        
    }
    //  Starts function that displays Todo so user can choose which item to edit 
    else if (UserChoice == "3")
    {
        // Displays ToDo list
        List<ToDoList> byDateToDo = myToDo!.OrderBy(x => x.DueDate).ToList();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"| {"Title".PadRight(24)} {"Project".PadRight(16)} {"DueDate".PadRight(12)} | {"Status"}");
        Console.WriteLine("---------------------------------------------------------");
        foreach (var task in byDateToDo)
        {
            Console.WriteLine($"| {task.Title.PadRight(24)} {task.Project.PadRight(16)} {task.DueDate.ToString("yyyy-MM-dd").PadRight(12)} | {task.Status}");
        }
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("---------------------------------------");
        Console.WriteLine();
        Console.WriteLine("Please enter the TITLE of the task you wish to edit:");
        // Asks user to type which ToDo item to edit
        string editChoice = (Console.ReadLine() ?? "");
        Console.WriteLine("");
        var editCheck = myToDo!.FirstOrDefault(x => x.Title == editChoice);
        // If the user entry matches a list item runs through the edit function
        if (editCheck != null)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Please UPDATE the task title:");
            editCheck.Title = (Console.ReadLine() ?? "");
            Console.WriteLine("Please UPDATE the project name:");
            editCheck.Project = (Console.ReadLine() ?? "");
            Console.WriteLine("Please UPDATE the task's due date (YYYY-MM-DD):");
            bool isCorrect = false;
            string userTime = "";
            DateTime dueDate = new DateTime();
            while (!isCorrect)
            {
                userTime = (Console.ReadLine() ?? "").Trim().ToLower();
                if (DateTime.TryParse(userTime, out dueDate))
                {
                    isCorrect = true;
                    editCheck.DueDate = dueDate;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("INVALID DATE");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            Console.WriteLine("Has the task been completed Y/n?:");
            string userEntry = (Console.ReadLine() ?? "").Trim().ToLower(); ;
            if (userEntry == "y")
            {
                editCheck.Status = "Complete";
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("-----------------");
                Console.WriteLine("UPDATE SUCCESSFUL");
                Console.WriteLine("-----------------");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                editCheck.Status = "";
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("TASK NOT FOUND, please choose a new option:");
            Console.ForegroundColor = ConsoleColor.White;
        }
            
    }
    //  Starts function that displays Todo so user can choose which item to delete 
    else if (UserChoice == "4")
    {
        // Displays ToDo list
        List<ToDoList> byDateToDo = myToDo!.OrderBy(x => x.DueDate).ToList();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"| {"Title".PadRight(24)} {"Project".PadRight(16)} {"DueDate".PadRight(12)} | {"Status"}");
        Console.WriteLine("---------------------------------------------------------");
        foreach (var task in byDateToDo)
        {
            Console.WriteLine($"| {task.Title.PadRight(24)} {task.Project.PadRight(16)} {task.DueDate.ToString("yyyy-MM-dd").PadRight(12)} | {task.Status}");
        }
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("---------------------------------------");
        Console.WriteLine();
        // Asks user to type which task to delete
        Console.WriteLine("Please enter the TITLE of the task you wish to Delete:");
        string deleteChoice = (Console.ReadLine() ?? "");
        var deleteCheck = myToDo!.FirstOrDefault(x => x.Title == deleteChoice);
        if (deleteCheck != null)
        {
            myToDo!.Remove(deleteCheck);
        }
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("-----------------");
        Console.WriteLine("DELETE SUCCESSFUL");
        Console.WriteLine("-----------------");
        Console.ForegroundColor = ConsoleColor.White;
    }
    // Saves the new Todo list to a json file and quits program
    else if (UserChoice == "5")
    {
        string jsonString = JsonSerializer.Serialize(myToDo);
        File.WriteAllText("ToDo.json", jsonString);
        RunProgram = false;
        Console.WriteLine();
        Console.WriteLine("------------------------------------------------");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("Thank you for using TODO-LIST, See you next time");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("------------------------------------------------");
        Console.WriteLine();
    }
    else
    {
        Console.WriteLine("You must choose a number between 1 - 5");
    }
}