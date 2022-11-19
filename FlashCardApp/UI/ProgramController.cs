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

        Console.WriteLine("Type Stack Name and hit Enter to Perform Operations on a Stack: ");
        Console.WriteLine("back to Go Back");
        var choice = Input.GetInput();
        if (choice == "back")
        {
            Console.Clear();
            return;
        }

        FlashCardOperations(new Stack { StackName = choice });
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

    private static void FlashCardOperations(Stack stack)
    {
        Console.Clear();
        FlashCardMenu();
        var choice = Input.GetChoice();

        while (choice != "back")
        {
            switch (choice)
            {
                case "play":
                    break;
                case "settings":
                    FlashCardSettings(stack);
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
    }

    private static void AddFlashCardToStack(Stack stack)
    {
        Console.Clear();

        Console.Write("Enter front content of FlashCard: ");
        var front = Input.GetInput();

        Console.Write("Enter back content of FlashCard: ");
        var back = Input.GetInput();

        var flashcard = new FlashCard { FrontContent = front, BackContent = back };

        DbManager.AddNewFlashCard(flashcard, stack);
        Console.Clear();
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
                    EditFrontAndBack(stack);
                    break;
                case "edit front":
                    EditFront(stack);
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

    private static void EditFrontAndBack(Stack stack)
    {
        ViewAllFlashCard();
        Console.Write("Enter name of flashcard to edit: ");
        var name = Input.GetChoice();

        Console.Write("Enter new content for front: ");
        var front = Input.GetInput();

        Console.Write("Enter new content for back: ");
        var back = Input.GetInput();

        var oldFlashcard = new FlashCard { FlashCardName = name };
        var newFlashCard = new FlashCard { FrontContent = front, BackContent = back };

        DbManager.UpdateFlashCard(oldFlashcard, newFlashCard, stack);
    }

    private static void EditFront(Stack stack)
    {
        ViewAllFlashCard();
        Console.Write("Enter name of flashcard to edit: ");
        var name = Input.GetChoice();

        Console.Write("Enter new content for front: ");
        var front = Input.GetInput();

        var oldFlashcard = new FlashCard { FlashCardName = name };
        var newFlashCard = new FlashCard { FrontContent = front };

        DbManager.UpdateFlashCardFront(oldFlashcard, newFlashCard, stack);
        Console.Clear();
    }

    private static void EditBack(Stack stack)
    {
        ViewAllFlashCard();
        Console.Write("Enter name of flashcard to edit: ");
        var name = Input.GetChoice();

        Console.Write("Enter new content for back: ");
        var back = Input.GetInput();

        var oldFlashcard = new FlashCard { FlashCardName = name };
        var newFlashCard = new FlashCard { BackContent = back };

        DbManager.UpdateFlashCardBack(oldFlashcard, newFlashCard, stack);
        Console.Clear();
    }

    private static void DeleteFlashCard(Stack stack)
    {
        ViewAllFlashCard();
        Console.Write("Enter front content of flashcard to delete: ");
        var front = Input.GetInput();

        var flashCard = new FlashCard { FrontContent = front };

        DbManager.DeleteFlashCard(flashCard, stack);
        Console.Clear();
    }

    private static void ViewAllFlashCard()
    {
        DisplayTable.ViewFlashCards();
    }
}