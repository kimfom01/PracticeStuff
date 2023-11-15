using BusinessLogic.Enums;
using BusinessLogic.Input;
using BusinessLogic.TableVisualizer;
using DataAccess.Dtos;
using DataAccess.Models;
using DataAccess.Repositories;
using Spectre.Console;

namespace BusinessLogic.Services.Implementation;

public class StackService : IStackService
{
    private readonly UserInput _input;
    private readonly IStackRepository _stackRepository;
    private readonly VisualizationService<StackDto> _displayTable;
    private readonly IFlashCardService _flashCardService;

    public StackService(
        UserInput input,
    IStackRepository stackRepository,
    VisualizationService<StackDto> displayTable,
    IFlashCardService flashCardService)
    {
        _input = input;
        _stackRepository = stackRepository;
        _displayTable = displayTable;
        _flashCardService = flashCardService;
    }

    public StackSettingsOptions DisplayStackSettingsMenu()
    {
        var choice = AnsiConsole.Prompt(new SelectionPrompt<StackSettingsOptions>()
            .Title("Select an option ([grey]Move up and down and hit enter to select[/])")
            .AddChoices(
                StackSettingsOptions.View,
                StackSettingsOptions.Create,
                StackSettingsOptions.Edit,
                StackSettingsOptions.Delete,
                StackSettingsOptions.Cancel));

        return choice;
    }

    public async Task ManageStacksSettings()
    {
        Console.Clear();
        
        var choice = DisplayStackSettingsMenu();

        while (choice != StackSettingsOptions.Cancel)
        {
            switch (choice)
            {
                case StackSettingsOptions.Create:
                    await GetStackToAdd();
                    break;
                case StackSettingsOptions.Edit:
                    await UpdateStackName();
                    break;
                case StackSettingsOptions.Delete:
                    await DeleteStack();
                    break;
                case StackSettingsOptions.View:
                    await ViewStackForFlashCardOperations();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Wrong input!");
                    break;
            }

            choice = DisplayStackSettingsMenu();
        }

        Console.Clear();
    }

    public async Task GetStackToAdd()
    {
        Console.Clear();

        Console.Write("Enter name to create or back to cancel: ");
        var name = _input.GetInput();
        if (name.ToLower() == "back")
        {
            Console.Clear();
            return;
        }

        await _stackRepository.AddNewStack(new Stack { Name = name });
        Console.Clear();
    }

    public void DisplayUpdateStackMenu()
    {
        Console.WriteLine("type the name of stack you want to rename");
        Console.WriteLine("back to Go Back");
        Console.Write("\nYour choice? ");
    }

    public async Task UpdateStackName()
    {
        var stackList = await _stackRepository.GetStacks();
        _displayTable.DisplayTable(stackList.ToList(), columnName: "Lessons", "");
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
            await _stackRepository.UpdateStack(new Stack { Name = choice }, new Stack { Name = newStackName });

            stackList = await _stackRepository.GetStacks();
            _displayTable.DisplayTable(stackList.ToList(), columnName: "Lessons", "");
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

    public async Task DeleteStack()
    {
        var stackList = await _stackRepository.GetStacks();
        _displayTable.DisplayTable(stackList.ToList(), columnName: "Lessons", "");
        DisplayDeleteMenu();
        var choice = _input.GetChoice();

        while (choice != "back")
        {
            await _stackRepository.DeleteStack(new Stack { Name = choice });

            stackList =await  _stackRepository.GetStacks();
            _displayTable.DisplayTable(stackList.ToList(), columnName: "Lessons", "");
            DisplayDeleteMenu();
            choice = _input.GetChoice();
        }

        Console.Clear();
    }

    public async Task ViewStackForFlashCardOperations()
    {
        Console.Clear();

        var stackList =await _stackRepository.GetStacks();
        _displayTable.DisplayTable(stackList.ToList(), columnName: "Lessons", "");

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

    public async Task CreateStackTable()
    {
        await _stackRepository.CreateStackTable();
    }
}