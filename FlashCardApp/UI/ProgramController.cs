using FlashCardApp.Data;
using FlashCardApp.Input;
using FlashCardApp.Models;

namespace FlashCardApp.UI;

public static class ProgramController
{
    private static readonly DatabaseManager DbManager = new();
    private static readonly UserInput Input = new();
    private static readonly TableVisualizationEngine DisplayTable = new();

    private static void MainMenu()
    {
        Console.WriteLine("MAIN MENU");
        Console.WriteLine("-------------------------------------");
        Console.WriteLine("What would you like to do?\n");
        Console.WriteLine("study to go to Study Area");
        Console.WriteLine("settings to go to Settings");
        Console.WriteLine("exit to End Program");
        Console.WriteLine("\nType your choice and hit Enter");
        Console.Write("Your choice? ");
    }

    private static void CreateTables()
    {
        DbManager.CreateStackTable();
        DbManager.CreateFlashCardTable();
        DbManager.CreateStudyAreaTable();
    }

    public static void StartProgram()
    {
        CreateTables();

        MainMenu();
        var choice = Input.GetChoice();

        while (choice != "exit")
        {
            switch (choice)
            {
                case "study":
                    StudyArea();
                    break;
                case "settings":
                    Settings();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Wrong input!");
                    break;
            }

            MainMenu();
            choice = Input.GetChoice();
        }
    }

    // Study Area Operations
    private static void StudyAreaMenu()
    {
        Console.WriteLine("STUDY AREA\n");
        Console.WriteLine("new to Start a New Lesson");
        Console.WriteLine("history to View History");
        Console.WriteLine("back to Go Back");
        Console.WriteLine("\nType your choice and hit Enter");
        Console.Write("Your choice? ");
    }

    private static void StudyArea()
    {
        Console.Clear();

        StudyAreaMenu();
        var choice = Input.GetChoice();

        while (choice != "back")
        {
            switch (choice)
            {
                case "new":
                    StartLesson();
                    break;
                case "history":
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Wrong input!");
                    break;
            }

            StudyAreaMenu();
            choice = Input.GetChoice();
        }

        Console.Clear();
    }

    private static void NewLessonMenu()
    {
        Console.WriteLine("Type a Stack Name to choose or back to Go Back");
        Console.Write("Your choice? ");
    }

    private static void StartLesson()
    {
        NewLessonMenu();
        var choice = Input.GetInput();
        if (choice == "back")
        {
            return;
        }

        var flashCards = DbManager.GetFlashCardsOfStack(new Stack { Name = choice });

        foreach (var card in flashCards)
        {
            Console.WriteLine(card.Name);
            Console.Write("Your answer: ");
            var answer = Input.GetInput();

            if (answer.ToLower() != card.Content.ToLower())
            {
                Console.WriteLine("Incorrect!");
                Console.WriteLine("Correct answer is " + card.Content);
                continue;
            }

            Console.WriteLine("Correct");
            // Increase the score
        }
        
        // Display the total score
    }

    // Settings
    private static void DisplaySettingsMenu()
    {
        Console.WriteLine("SETTINGS\n");
        Console.WriteLine("create to Create a New Stack");
        Console.WriteLine("rename to Rename a Stack");
        Console.WriteLine("delete to Delete a Stack");
        Console.WriteLine("view to View List of Stacks");
        Console.WriteLine("back to Go Back");
        Console.WriteLine("\nType your choice and hit Enter");
        Console.Write("Your choice? ");
    }

    private static void Settings()
    {
        Console.Clear();

        DisplaySettingsMenu();
        var choice = Input.GetChoice();

        while (choice != "back")
        {
            switch (choice)
            {
                case "create":
                    GetStackToAdd();
                    break;
                case "rename":
                    UpdateStackName();
                    break;
                case "delete":
                    DeleteStack();
                    break;
                case "view":
                    ViewStackForFlashCardOperations();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Wrong input!");
                    break;
            }

            DisplaySettingsMenu();
            choice = Input.GetChoice();
        }
    }

    // Stack Operations
    private static void GetStackToAdd()
    {
        Console.Clear();

        Console.Write("Enter name to create: ");
        var stackName = Input.GetInput();

        DbManager.AddNewStack(new Stack { Name = stackName });
        Console.Clear();
    }

    private static void DisplayUpdateStackMenu()
    {
        Console.WriteLine("type the name of stack you want to rename");
        Console.WriteLine("back to Go Back");
        Console.Write("\nYour choice? ");
    }

    private static void UpdateStackName()
    {
        ViewStackForFlashCardOperations();
        DisplayUpdateStackMenu();
        var choice = Input.GetChoice();
        while (choice != "back")
        {
            Console.Write("Enter new name");
            var newStack = Input.GetInput();

            DbManager.UpdateStack(new Stack { Name = choice }, new Stack { Name = newStack });

            ViewStackForFlashCardOperations();
            DisplayUpdateStackMenu();
            choice = Input.GetChoice();
        }

        Console.Clear();
    }

    private static void DisplayDeleteMenu()
    {
        Console.WriteLine("type the name of stack you want to delete");
        Console.WriteLine("back to Go Back");
        Console.Write("\nYour choice? ");
    }

    private static void DeleteStack()
    {
        ViewStackForFlashCardOperations();
        DisplayDeleteMenu();
        var choice = Input.GetChoice();

        while (choice != "back")
        {
            DbManager.DeleteStack(new Stack { Name = choice });

            ViewStackForFlashCardOperations();
            DisplayDeleteMenu();
            choice = Input.GetChoice();
        }

        Console.Clear();
    }

    private static void ViewStackForFlashCardOperations()
    {
        DisplayTable.ViewStacks();

        SelectStackToOperateOn();
    }

    private static void SelectStackToOperateOn()
    {
        Console.WriteLine("Type Stack Name and hit Enter to Perform Operations on a Stack: ");
        Console.WriteLine("back to Go Back");
        var choice = Input.GetInput();
        if (choice == "back")
        {
            Console.Clear();
            return;
        }

        FlashCardSettings(new Stack { Name = choice });
    }

    // FlashCard Operations

    private static void FlashCardSettingsMenu()
    {
        Console.WriteLine("add to Add a New FlashCard");
        Console.WriteLine("edit to Edit a FlashCard");
        Console.WriteLine("delete to Delete a FlashCard");
        Console.WriteLine("back to Go Back");
        Console.WriteLine("\nType your choice and hit Enter");
        Console.Write("Your choice? ");
    }

    private static void FlashCardSettings(Stack stack)
    {
        Console.Clear();
        FlashCardSettingsMenu();
        var choice = Input.GetChoice();

        while (choice != "back")
        {
            switch (choice)
            {
                case "add":
                    AddFlashCardToStack(stack);
                    break;
                case "edit":
                    EditFlashCard(stack);
                    break;
                case "delete":
                    DeleteFlashCard(stack);
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Wrong input!");
                    break;
            }

            FlashCardSettingsMenu();
            choice = Input.GetChoice();
        }

        Console.Clear();
    }

    private static void AddFlashCardToStack(Stack stack)
    {
        Console.Clear();

        Console.Write("Enter name of FlashCard to add or back to cancel: ");
        var name = Input.GetInput();
        if (name.ToLower() == "back")
        {
            Console.Clear();
            return;
        }

        Console.Write("Enter content of FlashCard: ");
        var content = Input.GetInput();

        var flashcard = new FlashCard { Name = name, Content = content };

        DbManager.AddNewFlashCard(flashcard, stack);
        Console.Clear();
    }

    private static void EditFlashCardMenu()
    {
        Console.WriteLine("all to Edit Front and Back");
        Console.WriteLine("edit front to Edit Front");
        Console.WriteLine("edit back to Edit Back");
        Console.WriteLine("back to Go Back");
        Console.WriteLine("\nType your choice and hit Enter");
        Console.Write("Your choice? ");
    }

    private static void EditFlashCard(Stack stack)
    {
        Console.Clear();
        EditFlashCardMenu();
        var choice = Input.GetChoice();

        while (choice != "back")
        {
            switch (choice)
            {
                case "all":
                    EditAll(stack);
                    break;
                case "edit front":
                    EditFlashCardName(stack);
                    break;
                case "edit back":
                    EditBack(stack);
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Wrong input!");
                    break;
            }

            EditFlashCardMenu();
            choice = Input.GetChoice();
        }

        Console.Clear();
    }

    private static void EditAll(Stack stack)
    {
        ViewAllFlashCard(stack);
        Console.Write("Enter name of FlashCard to edit or back to cancel: ");
        var name = Input.GetChoice();
        if (name.ToLower() == "back")
        {
            Console.Clear();
            return;
        }

        Console.Write("Enter new name for FlashCard: ");
        var newName = Input.GetInput();

        Console.Write("Enter new content for FlashCard: ");
        var newContent = Input.GetInput();

        var oldFlashcard = new FlashCard { Name = name };
        var newFlashCard = new FlashCard { Name = newName, Content = newContent };

        DbManager.UpdateFlashCard(oldFlashcard, newFlashCard, stack);
        Console.Clear();
    }

    private static void EditFlashCardName(Stack stack)
    {
        ViewAllFlashCard(stack);
        Console.Write("Enter name of FlashCard to edit or back to cancel: ");
        var name = Input.GetChoice();
        if (name.ToLower() == "back")
        {
            Console.Clear();
            return;
        }

        Console.Write("Enter new name for FlashCard: ");
        var newName = Input.GetInput();

        var oldFlashcard = new FlashCard { Name = name };
        var newFlashCard = new FlashCard { Name = newName };

        DbManager.UpdateFlashCardName(oldFlashcard, newFlashCard, stack);
        Console.Clear();
    }

    private static void EditBack(Stack stack)
    {
        ViewAllFlashCard(stack);
        Console.Write("Enter name of FlashCard to edit or back to cancel: ");
        var name = Input.GetChoice();
        if (name.ToLower() == "back")
        {
            Console.Clear();
            return;
        }

        Console.Write("Enter new content for FlashCard: ");
        var newContent = Input.GetInput();

        var oldFlashcard = new FlashCard { Name = name };
        var newFlashCard = new FlashCard { Content = newContent };

        DbManager.UpdateFlashCardContent(oldFlashcard, newFlashCard, stack);
        Console.Clear();
    }

    private static void DeleteFlashCard(Stack stack)
    {
        ViewAllFlashCard(stack);
        Console.Write("Enter name of FlashCard to edit or back to cancel: ");
        var name = Input.GetChoice();
        if (name.ToLower() == "back")
        {
            Console.Clear();
            return;
        }

        var flashCard = new FlashCard { Name = name };

        DbManager.DeleteFlashCard(flashCard, stack);
        Console.Clear();
    }

    private static void ViewAllFlashCard(Stack stack)
    {
        DisplayTable.ViewFlashCards(stack);
    }
}