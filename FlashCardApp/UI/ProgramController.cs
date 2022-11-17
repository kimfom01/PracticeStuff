using FlashCardApp.Data;
using FlashCardApp.Input;
using FlashCardApp.Models;

namespace FlashCardApp.UI;

public class ProgramController
{
    private static readonly DatabaseManager DbManager = new();
    private static readonly UserInput Input = new();

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

    private static void DisplayDeleteMenu()
    {
        Console.WriteLine("back to Go Back");
        Console.WriteLine("delete to Delete Record");
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
                    break;
                case "delete":
                    break;
                case "view":
                    break;
                default:
                    Console.WriteLine("Wrong input!");
                    break;
            }
            
            DisplayMainMenu();
            choice = Input.GetChoice();
        }
    }

    static void GetStackToAdd()
    {
        Console.Clear();
        
        var stackName = Input.GetInput();

        DbManager.AddNewStack(new Stack { StackName = stackName });

        DisplayMainMenu();
    }
}