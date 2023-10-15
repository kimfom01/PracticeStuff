using BusinessLogic.Input;
using BusinessLogic.TableVisualizer;
using DataAccess.Data;
using DataAccess.DTO;
using DataAccess.Models;

namespace BusinessLogic.Services.Implementation;

public class StackService : IStackService
{
    private readonly UserInput _input;
    private readonly IStackDataManager _stackDataManager;
    private readonly VisualizationService<StackDTO> _displayTable;
    private readonly IFlashCardService _flashCardService;

    public StackService(
        UserInput input,
    IStackDataManager stackDataManager,
    VisualizationService<StackDTO> displayTable,
    IFlashCardService flashCardService)
    {
        _input = input;
        _stackDataManager = stackDataManager;
        _displayTable = displayTable;
        _flashCardService = flashCardService;
    }

    public void DisplayStackSettingsMenu()
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

    public void ManageStacksSettings()
    {
        Console.Clear();

        DisplayStackSettingsMenu();
        var choice = _input.GetChoice();

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

            DisplayStackSettingsMenu();
            choice = _input.GetChoice();
        }

        Console.Clear();
    }

    public void GetStackToAdd()
    {
        Console.Clear();

        Console.Write("Enter name to create or back to cancel: ");
        var name = _input.GetInput();
        if (name.ToLower() == "back")
        {
            Console.Clear();
            return;
        }

        _stackDataManager.AddNewStack(new Stack { Name = name });
        Console.Clear();
    }

    public void DisplayUpdateStackMenu()
    {
        Console.WriteLine("type the name of stack you want to rename");
        Console.WriteLine("back to Go Back");
        Console.Write("\nYour choice? ");
    }

    public void UpdateStackName()
    {
        var stackList = _stackDataManager.GetStacks();
        _displayTable.DisplayTable(stackList, columnName: "Lessons", "");
        DisplayUpdateStackMenu();
        var choice = _input.GetChoice();
        while (choice != "back")
        {
            Console.Write("Enter new name or back to cancel: ");
            var newStackName = _input.GetInput();
            if (newStackName.ToLower() == "back")
            {
                Console.Clear();
                return;
            }
            _stackDataManager.UpdateStack(new Stack { Name = choice }, new Stack { Name = newStackName });

            stackList = _stackDataManager.GetStacks();
            _displayTable.DisplayTable(stackList, columnName: "Lessons", "");
            DisplayUpdateStackMenu();
            choice = _input.GetChoice();
        }

        Console.Clear();
    }

    public void DisplayDeleteMenu()
    {
        Console.WriteLine("type the name of stack you want to delete");
        Console.WriteLine("back to Go Back");
        Console.Write("\nYour choice? ");
    }

    public void DeleteStack()
    {
        var stackList = _stackDataManager.GetStacks();
        _displayTable.DisplayTable(stackList, columnName: "Lessons", "");
        DisplayDeleteMenu();
        var choice = _input.GetChoice();

        while (choice != "back")
        {
            _stackDataManager.DeleteStack(new Stack { Name = choice });

            stackList = _stackDataManager.GetStacks();
            _displayTable.DisplayTable(stackList, columnName: "Lessons", "");
            DisplayDeleteMenu();
            choice = _input.GetChoice();
        }

        Console.Clear();
    }

    public void ViewStackForFlashCardOperations()
    {
        Console.Clear();

        var stackList = _stackDataManager.GetStacks();
        _displayTable.DisplayTable(stackList, columnName: "Lessons", "");

        SelectStackToOperateOn();
        Console.Clear();
    }

    public void SelectStackToOperateOn()
    {
        Console.WriteLine("Type Stack Name and hit Enter to Perform Operations on a Stack: ");
        Console.WriteLine("back to Go Back");
        var name = _input.GetInput();
        if (name == "back")
        {
            Console.Clear();
            return;
        }

        _flashCardService.ManageFlashCardSettings(new Stack { Name = name });
    }

    public void CreateStackTable()
    {
        _stackDataManager.CreateStackTable();
    }
}