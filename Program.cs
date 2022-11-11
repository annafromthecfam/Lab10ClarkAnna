string? navigationResponse;
string itemTitle;
string userConfirmation;
string itemDescription;
string lastItemNumber;
int indexNumberForAllItems;
DateTime itemStart = new DateTime();
List<Tuple<string, string, DateTime>> allItems = new List<Tuple<string, string, DateTime>>();

ToDoList();

void ToDoList()
{
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("To-Do List");
    Console.WriteLine("1) View list");
    Console.WriteLine("2) Add new item to list");
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("Please respond by typing '1' or '2'");
    Console.ResetColor();
    navigationResponse = Console.ReadLine();

    if (navigationResponse == "1")
    {
        Console.Clear();
        ViewList();
    }

    else if (navigationResponse == "2")
    {
        Console.Clear();
        AddNewItem();
    }

    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Invalid Response.");
        Console.ResetColor();
        Thread.Sleep(1000);
        ToDoList();
    }
}

void AddNewItem()
{
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine("Please enter title of to-do item");
    Console.ResetColor();
    itemTitle = Console.ReadLine();
    ConfirmTitle();
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine($"Please enter a description for {itemTitle}");
    Console.ResetColor();
    itemDescription = Console.ReadLine();
    ConfirmDescription();
    itemStart = DateTime.Now;
    allItems.Add(Tuple.Create(itemTitle, itemDescription, itemStart));
    indexNumberForAllItems = allItems.Count - 1;
    lastItemNumber = Convert.ToString(allItems[indexNumberForAllItems]);
    File.AppendAllText("./ToDoList.csv", $"{lastItemNumber}\n");
    Console.ForegroundColor = ConsoleColor.Green;
    ToDoList();
}

void ConfirmTitle()
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
    }

    if (userConfirmation == "N" || userConfirmation == "n")
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"{itemTitle} has been deleted");
        Console.ResetColor();
        Thread.Sleep(1000);
        Console.Clear();
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
            ToDoList();
        }

        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("We weren't sure what you wanted. Here's the main menu...");
            Console.ResetColor();
            Thread.Sleep(1000);
            ToDoList();
        }
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

void ViewList()
{
    Console.WriteLine(File.ReadAllText("./ToDoList.csv"));
}