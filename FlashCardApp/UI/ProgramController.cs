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
                    Console.WriteLine("Wrong input!");
                    break;
            }

            DisplayMainMenu();
            choice = Input.GetChoice();
        }
    }

    private static void GetStackToAdd()
    {
        Console.Clear();

        var stackName = Input.GetInput();

        DbManager.AddNewStack(new Stack { StackName = stackName });

        DisplayMainMenu();
    }

    private static void DisplayUpdateStackMenu()
    {
        Console.WriteLine("type the name of stack you want to rename");
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
            var newStack = Input.GetInput();

            DbManager.UpdateStack(new Stack { StackName = choice }, new Stack { StackName = newStack });

            ViewAllStacks();
            DisplayUpdateStackMenu();
            choice = Input.GetChoice();
        }
    }

    private static void DisplayDeleteMenu()
    {
        Console.WriteLine("type the name of stack you want to rename");
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
    }

    private static void ViewAllStacks()
    {
        DisplayTable.ViewStacks();
    }
}