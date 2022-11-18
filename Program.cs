using System.IO;

List<Tuple<string, string, string, string>> allItems = new List<Tuple<string, string, string, string>>();
List<Tuple<string, string, string, string>> toDoItems = new List<Tuple<string, string, string, string>>();
List<Tuple<string, string, string, string>> completedItems = new List<Tuple<string, string, string, string>>();

string[] allItemsLines = File.ReadAllLines("./AllItems.csv");
string[] completedItemLines = File.ReadAllLines("./CompletedItems.csv");
string[] toDoItemsLines = File.ReadAllLines("./ToDoItems.csv");

string[] greetingUser = new string[10] {"Howdy", "Hello", "Good to see you!", "Welcome", "Hi", "Greetings", "'sup", "Hola", "Guten Tag", "Aloha"};
string navigationResponse;
string itemTitle;
string itemDescription;
DateTime itemStart;
string itemEnd;
string userConfirmation;
int itemsCreatedToday = 0;
Random rand = new Random();
int greetingIndex = rand.Next(0, 9);

CreateLists();
ToDoProgram();

void CreateLists()
{
    foreach (string line in allItemsLines)
    {
        string[] info = line.Split(',');
        allItems.Add(Tuple.Create(info[0], info[1], info[2], info[3]));
    }

    foreach (string line in toDoItemsLines)
    {
        string[] info = line.Split(',');
        toDoItems.Add(Tuple.Create(info[0], info[1], info[2], info[3]));
    }

    foreach (string line in completedItemLines)
    {
        string[] info = line.Split(',');
        completedItems.Add(Tuple.Create(info[0], info[1], info[2], info[3]));
    }
}

void ToDoProgram()
{
    Console.ForegroundColor = ConsoleColor.White;
    Console.Clear();
    Console.WriteLine(greetingUser[greetingIndex]);
    Console.WriteLine("To-Do List");
    Console.WriteLine("1) View All Items");
    Console.WriteLine("2) View To-Do Items");
    Console.WriteLine("3) View Completed Items");
    Console.WriteLine("4) Add new item to list");
    Console.WriteLine("5) View Stats");
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("Please respond by typing '1' or '2'");
    navigationResponse = Console.ReadLine();
    Console.ResetColor();

    if (navigationResponse == "1")
    {
        Console.Clear();
        ViewAllItemsList();
    }

    else if (navigationResponse == "2")
    {
        Console.Clear();
        ViewToDoItemsList();
    }

    else if (navigationResponse == "3")
    {
        Console.Clear();
        ViewCompletedItemsList();
    }

    else if (navigationResponse == "4")
    {
        Console.Clear();
        AddNewItem();
    }

    else if (navigationResponse == "5")
    {
        Console.Clear();
        ViewStats();
    }

    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Invalid Response.");
        Console.ResetColor();
        Thread.Sleep(1000);
        ToDoProgram();
    }
}

void AddNewItem()
{
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("Please enter title of to-do item");
    Console.ResetColor();
    itemTitle = Console.ReadLine();
    if (ConfirmTitle())
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"Please enter a description for {itemTitle}");
        Console.ResetColor();
        itemDescription = Console.ReadLine();
        ConfirmDescription();
        itemStart = DateTime.Now;
        itemEnd = "N/A";
        allItems.Add(Tuple.Create(itemTitle, itemDescription, Convert.ToString(itemStart), itemEnd));
        toDoItems.Add(Tuple.Create(itemTitle, itemDescription, Convert.ToString(itemStart), itemEnd));
        File.AppendAllText("AllItems.csv", $"{allItems[allItems.Count - 1]}\n");
        File.AppendAllText("ToDoItems.csv", $"{toDoItems[toDoItems.Count - 1]}\n");
        itemsCreatedToday++;
    }

    else
    {
        Console.WriteLine("Would you like to...");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("1) Add a new item to list? or");
        Console.WriteLine("2) Return to main menu?");
        Console.ResetColor();
        Console.WriteLine("Please respond by typing '1' or '2'");
        navigationResponse = Console.ReadLine();
        if (navigationResponse == "1")
        {
            AddNewItem();
        }

        else if (navigationResponse == "2")
        {
            ToDoProgram();
        }

        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("We weren't sure what you wanted. Here's the main menu...");
            Console.ResetColor();
            Thread.Sleep(1000);
            ToDoProgram();
        }
    }
    ToDoProgram();
}

bool ConfirmTitle()
{
    Console.Write("Is ");
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.Write(itemTitle);
    Console.ResetColor();
    Console.WriteLine(" the correct title? 'Y' or 'N'");
    userConfirmation = Console.ReadLine();
    if (userConfirmation == "Y" || userConfirmation == "y")
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Title confirmed: {itemTitle}");
        Console.ResetColor();
        Thread.Sleep(1000);
        Console.Clear();
        return true;
    }

    if (userConfirmation == "N" || userConfirmation == "n")
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"{itemTitle} has been deleted");
        Console.ResetColor();
        Thread.Sleep(1000);
        Console.Clear();
        return false;
    }

    else
    {
        Console.WriteLine("Invalid Response.");
        return false;
    }
}

void ConfirmDescription()
{
    Console.Write("Is ");
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.Write(itemDescription);
    Console.ResetColor();
    Console.WriteLine(" the correct description? 'Y' or 'N'");
    userConfirmation = Console.ReadLine();
    if (userConfirmation == "Y" || userConfirmation == "y")
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Description confirmed.");
        Console.ResetColor();
        Thread.Sleep(1000);
    }

    if (userConfirmation == "N" || userConfirmation == "n")
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Description deleted.");
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"Please enter a description for {itemTitle}");
        Console.ResetColor();
        itemDescription = Console.ReadLine();
        ConfirmDescription();
    }
}

void ViewAllItemsList()
{
    foreach (var item in allItems)
    {
        Console.WriteLine(item);
    }
    Console.WriteLine("Press any key to return to main menu.");
    Console.ReadKey();
    ToDoProgram();
}

void ViewToDoItemsList()
{
    int p = 0;
    foreach (var item in toDoItems)
    {
        Console.WriteLine($"{p}. {item}");
        p++;
    }
    Console.WriteLine("To mark an item as complete, type the number associated with the item.");
    Console.WriteLine("Or press any other key and ENTER to return to the main menu.");
    navigationResponse = Console.ReadLine();
    if (navigationResponse == "0")
    {
        completedItems.Add(toDoItems[0]);
        toDoItems.Remove(toDoItems[0]);
        File.Delete("ToDoItems.csv");
        File.AppendAllText("CompletedItems.csv", $"{Convert.ToString(completedItems[completedItems.Count - 1])}\n");
        int x = 0;
        foreach (var item in toDoItems)
        {
            File.AppendAllText("toDoItems.csv", $"{Convert.ToString(toDoItems[x])}\n");
            x++;
        }
    }

    else if (navigationResponse == "1")
    {
        completedItems.Add(toDoItems[1]);
        toDoItems.Remove(toDoItems[1]);
        File.Delete("ToDoItems.csv");
        int x = 0;
        foreach (var item in toDoItems)
        {
            File.AppendAllText("toDoItems.csv", $"{Convert.ToString(toDoItems[x])}\n");
            x++;
        }
    }

    else if (navigationResponse == "2")
    {
        completedItems.Add(toDoItems[2]);
        toDoItems.Remove(toDoItems[2]);
        File.Delete("ToDoItems.csv");
        int x = 0;
        foreach (var item in toDoItems)
        {
            File.AppendAllText("toDoItems.csv", $"{Convert.ToString(toDoItems[x])}\n");
            x++;
        }
    }

    else if (navigationResponse == "3")
    {
        completedItems.Add(toDoItems[3]);
        toDoItems.Remove(toDoItems[3]);
        File.Delete("ToDoItems.csv");
        int x = 0;
        foreach (var item in toDoItems)
        {
            File.AppendAllText("toDoItems.csv", $"{Convert.ToString(toDoItems[x])}\n");
            x++;
        }
    }

    else if (navigationResponse == "4")
    {
        completedItems.Add(toDoItems[4]);
        toDoItems.Remove(toDoItems[4]);
        File.Delete("ToDoItems.csv");
        int x = 0;
        foreach (var item in toDoItems)
        {
            File.AppendAllText("toDoItems.csv", $"{Convert.ToString(toDoItems[x])}\n");
            x++;
        }
    }

    else if (navigationResponse == "5")
    {
        completedItems.Add(toDoItems[5]);
        toDoItems.Remove(toDoItems[5]);
        File.Delete("ToDoItems.csv");
        int x = 0;
        foreach (var item in toDoItems)
        {
            File.AppendAllText("toDoItems.csv", $"{Convert.ToString(toDoItems[x])}\n");
            x++;
        }
    }

    ToDoProgram();
}

void ViewCompletedItemsList()
{
    foreach (var item in completedItems)
    {
        Console.WriteLine(item);
    }
    Console.WriteLine("Press any key to return to main menu.");
    Console.ReadKey();
    ToDoProgram();
}

void MarkComplete(int x)
{
    itemEnd = Convert.ToString(DateTime.Now);
    completedItems.Add(toDoItems[x]);
    completedItems.Remove(toDoItems[x]);
    File.Delete("ToDoItems.csv");
    for (int y = 0; y < toDoItems.Count; y++)
    {
        File.AppendAllText("ToDoItems.csv", $"{Convert.ToString(toDoItems[x])}\n");
    }
    File.AppendAllText("CompletedItems.csv", $"{Convert.ToString(completedItems[completedItems.Count - 1])}\n");
}

void ViewStats()
{
    Console.WriteLine($"Total Items: {allItems.Count}");
    Console.WriteLine($"Total To-Do Items: {toDoItems.Count}");
    Console.WriteLine($"Total Completed Items: {completedItems.Count}");
    Console.WriteLine($"Today {itemsCreatedToday} items were created.");
    File.AppendAllText("Stats.txt", "______________________________________\n");
    File.AppendAllText("Stats.txt", $"Total Items: {allItems.Count}\n");
    File.AppendAllText("Stats.txt", $"Total To-Do Items: {toDoItems.Count}\n");
    File.AppendAllText("Stats.txt", $"Total Completed Items: {completedItems.Count}\n");
    File.AppendAllText("Stats.txt", $"Today {itemsCreatedToday} items were created.\n");
    Console.WriteLine("Press any key to return to main menu.");
    Console.ReadKey();
    ToDoProgram();
}