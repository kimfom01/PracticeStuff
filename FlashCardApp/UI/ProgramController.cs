using FlashCardApp.Data;
using FlashCardApp.Input;
using FlashCardApp.Models;

namespace FlashCardApp.UI;

public class ProgramController
{
    private static readonly DatabaseManager DbManager = new();
    private static readonly UserInput Input = new();
    private static readonly TableVisualizationEngine DisplayTable = new();

    private static void DisplayMainMenu()
    {
        Console.WriteLine("MAIN MENU");
        Console.WriteLine("-------------------------------------");
        Console.WriteLine("What would you like to do?");
        Console.WriteLine("create to Create a New Stack");
        Console.WriteLine("rename to Rename a Stack");
        Console.WriteLine("delete to Delete a Stack");
        Console.WriteLine("view to View List of Stacks");
        Console.WriteLine("exit to End Program");
        Console.WriteLine("Type your choice and hit Enter");
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

        DisplayMainMenu();
        var choice = Input.GetChoice();

        while (choice != "exit")
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
                    ViewAllStacks();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Wrong input!");
                    break;
            }

            DisplayMainMenu();
            choice = Input.GetChoice();
        }
    }

    // Stack Operations
    private static void GetStackToAdd()
    {
        Console.Clear();

        Console.Write("Enter name to create: ");
        var stackName = Input.GetInput();

        DbManager.AddNewStack(new Stack { StackName = stackName });
    }

    private static void DisplayUpdateStackMenu()
    {
        Console.WriteLine("type the NAME OF STACK YOU WANT TO RENAME");
        Console.WriteLine("back to Go Back");
        Console.Write("Your choice? ");
    }

    private static void UpdateStackName()
    {
        ViewAllStacks();
        DisplayUpdateStackMenu();
        var choice = Input.GetChoice();
        while (choice != "back")
        {
            Console.Write("Enter new name");
            var newStack = Input.GetInput();

            DbManager.UpdateStack(new Stack { StackName = choice }, new Stack { StackName = newStack });

            ViewAllStacks();
            DisplayUpdateStackMenu();
            choice = Input.GetChoice();
        }

        Console.Clear();
    }

    private static void DisplayDeleteMenu()
    {
        Console.WriteLine("type the name of stack you want to delete");
        Console.WriteLine("back to Go Back");
        Console.Write("Your choice? ");
    }

    private static void DeleteStack()
    {
        ViewAllStacks();
        DisplayDeleteMenu();
        var choice = Input.GetChoice();

        while (choice != "back")
        {
            DbManager.DeleteStack(new Stack { StackName = choice });

            ViewAllStacks();
            DisplayDeleteMenu();
            choice = Input.GetChoice();
        }

        Console.Clear();
    }

    private static void ViewAllStacks()
    {
        DisplayTable.ViewStacks();

        FlashCardMenu();
    }

    // FlashCard Operations
    private static void FlashCardMenu()
    {
        Console.WriteLine("FLASHCARD MENU");
        Console.WriteLine("-------------------------------------");
        Console.WriteLine("What would you like to do?");
        Console.WriteLine("play to Start Learning");
        Console.WriteLine("settings to Enter FlashCard Settings");
        Console.WriteLine("back to Go Back");
        Console.WriteLine("Type your choice and hit Enter");
        Console.Write("Your choice? ");
    }

    private static void FlashCardOperations()
    {
        FlashCardMenu();
        var choice = Input.GetChoice();

        while (choice != "back")
        {
            switch (choice)
            {
                case "play":
                    break;
                case "settings":
                    FlashCardSettings();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Wrong input!");
                    break;
            }

            FlashCardMenu();
            choice = Input.GetChoice();
        }
    }

    private static void FlashCardSettingsMenu()
    {
        Console.WriteLine("add to Add a New FlashCard");
        Console.WriteLine("edit to Edit a FlashCard");
        Console.WriteLine("delete to Delete a FlashCard");
        Console.WriteLine("back to Go Back");
        Console.WriteLine("Type your choice and hit Enter");
        Console.Write("Your choice? ");
    }

    private static void FlashCardSettings()
    {
        FlashCardSettingsMenu();
        var choice = Input.GetChoice();

        while (choice != "back")
        {
            switch (choice)
            {
                case "add":
                    AddFlashCardToStack();
                    break;
                case "edit":
                    EditFlashCard();
                    break;
                case "delete":
                    DeleteFlashCard();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Wrong input!");
                    break;
            }

            FlashCardSettingsMenu();
            choice = Input.GetChoice();
        }
    }

    private static void AddFlashCardToStack()
    {
        Console.Clear();

        (FlashCard flashcard, Stack stack) = GetStackAndModel();
        DbManager.AddNewFlashCard(flashcard, stack);
    }

    private static (FlashCard flashcard, Stack stack) GetStackAndModel()
    {
        DisplayTable.ViewStacks();
        Console.Write("Select a stack to add to: ");
        var choice = Input.GetChoice();

        Console.Write("Enter front content of FlashCard");
        var front = Input.GetInput();

        Console.Write("Enter back content of FlashCard");
        var back = Input.GetInput();

        var stack = new Stack { StackName = choice };
        var flashcard = new FlashCard { FrontContent = front, BackContent = back };

        return (flashcard, stack);
    }

    private static void EditFlashCardMenu()
    {
        Console.WriteLine("all to Edit Front and Back");
        Console.WriteLine("edit front to Edit Front");
        Console.WriteLine("edit back to Edit Back");
        Console.WriteLine("back to Go Back");
        Console.WriteLine("Type your choice and hit Enter");
        Console.Write("Your choice? ");
    }

    private static void EditFlashCard()
    {
        Console.Clear();
        EditFlashCardMenu();
        var choice = Input.GetChoice();

        while (choice != "back")
        {
            switch (choice)
            {
                case "all":
                    EditFrontAndBack();
                    break;
                case "edit front":
                    EditFront();
                    break;
                case "edit back":
                    EditBack();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Wrong input!");
                    break;
            }

            EditFlashCardMenu();
            choice = Input.GetChoice();
        }
    }

    private static void EditFrontAndBack()
    {
        Console.Write("Select Stack");
        var choice = Input.GetChoice();

        Console.Write("Enter name of flashcard to edit");
        var name = Input.GetChoice();

        Console.Write("Enter new content for front: ");
        var front = Input.GetInput();

        Console.Write("Enter new content for back: ");
        var back = Input.GetInput();

        var oldFlashcard = new FlashCard { FlashCardName = name };
        var newFlashCard = new FlashCard { FrontContent = front, BackContent = back };
        var stack = new Stack { StackName = choice };

        DbManager.UpdateFlashCard(oldFlashcard, newFlashCard, stack);
    }

    private static void EditFront()
    {
        Console.Write("Select Stack");
        var choice = Input.GetChoice();

        Console.Write("Enter name of flashcard to edit");
        var name = Input.GetChoice();

        Console.Write("Enter new content for front: ");
        var front = Input.GetInput();

        var oldFlashcard = new FlashCard { FlashCardName = name };
        var newFlashCard = new FlashCard { FrontContent = front };
        var stack = new Stack { StackName = choice };

        DbManager.UpdateFlashCard(oldFlashcard, newFlashCard, stack);
    }

    private static void EditBack()
    {
        Console.Write("Select Stack");
        var choice = Input.GetChoice();

        Console.Write("Enter name of flashcard to edit");
        var name = Input.GetChoice();

        Console.Write("Enter new content for back: ");
        var back = Input.GetInput();

        var oldFlashcard = new FlashCard { FlashCardName = name };
        var newFlashCard = new FlashCard { BackContent = back };
        var stack = new Stack { StackName = choice };

        DbManager.UpdateFlashCard(oldFlashcard, newFlashCard, stack);
    }

    private static void DeleteFlashCard()
    {
        throw new NotImplementedException();
    }
}