using BusinessLogic.Enums;
using BusinessLogic.Input;
using BusinessLogic.TableVisualizer;
using DataAccess.Data;
using DataAccess.DTO;
using DataAccess.Models;
using Spectre.Console;

namespace BusinessLogic.Services.Implementation;

public class FlashCardService : IFlashCardService
{
    private readonly UserInput _input;
    private readonly IFlashCardDataManager _flashCardDataManager;
    private readonly VisualizationService<FlashCardDTO> _displayTable;

    public FlashCardService(
        UserInput input,
    VisualizationService<FlashCardDTO> displayTable,
    IFlashCardDataManager flashCardDataManager)
    {
        _input = input;
        _displayTable = displayTable;
        _flashCardDataManager = flashCardDataManager;
    }

    public FlashCardSettingsOptions GetFlashCardSettingsChoice()
    {
        var choice = AnsiConsole.Prompt(new SelectionPrompt<FlashCardSettingsOptions>()
            .Title("Select an option ([grey]Move up and down and hit enter to select[/])")
            .AddChoices(
                FlashCardSettingsOptions.View,
                FlashCardSettingsOptions.Add,
                FlashCardSettingsOptions.Edit,
                FlashCardSettingsOptions.Delete,
                FlashCardSettingsOptions.Cancel));

        return choice;
    }

    public void ManageFlashCardSettings(Stack stack)
    {
        Console.Clear();
        var choice = GetFlashCardSettingsChoice();

        while (choice != FlashCardSettingsOptions.Cancel)
        {
            switch (choice)
            {
                case FlashCardSettingsOptions.View:
                    ViewFlashCards(stack);
                    break;
                case FlashCardSettingsOptions.Add:
                    AddFlashCardToStack(stack);
                    break;
                case FlashCardSettingsOptions.Edit:
                    EditFlashCard(stack);
                    break;
                case FlashCardSettingsOptions.Delete:
                    DeleteFlashCard(stack);
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Wrong input!");
                    break;
            }

            choice = GetFlashCardSettingsChoice();
        }

        Console.Clear();
    }

    public void ViewFlashCards(Stack stack)
    {
        Console.Clear();

        ViewFlashCardOfStack(stack);

        Console.WriteLine("Hit Enter to return to previous menu.");
        Console.ReadLine();
        Console.Clear();
    }

    public void AddFlashCardToStack(Stack stack)
    {
        Console.Clear();

        Console.Write("Enter name of FlashCard to add or back to cancel: ");
        var name = _input.GetInput();
        if (name.ToLower() == "back")
        {
            Console.Clear();
            return;
        }

        Console.Write("Enter content of FlashCard: ");
        var content = _input.GetInput();

        var flashcard = new FlashCard { Name = name, Content = content };

        _flashCardDataManager.AddNewFlashCard(flashcard, stack);
        Console.Clear();
    }

    public EditFlashCardOptions GetEditFlashCardChoice()
    {
        var choice = AnsiConsole.Prompt(new SelectionPrompt<EditFlashCardOptions>()
            .Title("Select an option ([grey]Move up and down and hit enter to select[/])")
            .AddChoices(
                EditFlashCardOptions.All,
                EditFlashCardOptions.Front,
                EditFlashCardOptions.Back,
                EditFlashCardOptions.Cancel));

        return choice;
    }

    public void EditFlashCard(Stack stack)
    {
        Console.Clear();

        var choice = GetEditFlashCardChoice();

        while (choice != EditFlashCardOptions.Cancel)
        {
            switch (choice)
            {
                case EditFlashCardOptions.All:
                    EditAll(stack);
                    break;
                case EditFlashCardOptions.Front:
                    EditFlashCardName(stack);
                    break;
                case EditFlashCardOptions.Back:
                    EditBack(stack);
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Wrong input!");
                    break;
            }

            choice = GetEditFlashCardChoice();
        }

        Console.Clear();
    }

    public void EditAll(Stack stack)
    {
        ViewFlashCardOfStack(stack);
        Console.Write("Enter name of FlashCard to edit or back to cancel: ");
        var name = _input.GetChoice();
        if (name.ToLower() == "back")
        {
            Console.Clear();
            return;
        }

        Console.Write("Enter new name for FlashCard: ");
        var newName = _input.GetInput();

        Console.Write("Enter new content for FlashCard: ");
        var newContent = _input.GetInput();

        var oldFlashcard = new FlashCard { Name = name };
        var newFlashCard = new FlashCard { Name = newName, Content = newContent };

        _flashCardDataManager.UpdateFlashCard(oldFlashcard, newFlashCard, stack);
        Console.Clear();
    }

    public void EditFlashCardName(Stack stack)
    {
        ViewFlashCardOfStack(stack);
        Console.Write("Enter name of FlashCard to edit or back to cancel: ");
        var name = _input.GetChoice();
        if (name.ToLower() == "back")
        {
            Console.Clear();
            return;
        }

        Console.Write("Enter new name for FlashCard: ");
        var newName = _input.GetInput();

        var oldFlashcard = new FlashCard { Name = name };
        var newFlashCard = new FlashCard { Name = newName };

        _flashCardDataManager.UpdateFlashCardName(oldFlashcard, newFlashCard, stack);
        Console.Clear();
    }

    public void EditBack(Stack stack)
    {
        ViewFlashCardOfStack(stack);
        Console.Write("Enter name of FlashCard to edit or back to cancel: ");
        var name = _input.GetChoice();
        if (name.ToLower() == "back")
        {
            Console.Clear();
            return;
        }

        Console.Write("Enter new content for FlashCard: ");
        var newContent = _input.GetInput();

        var oldFlashcard = new FlashCard { Name = name };
        var newFlashCard = new FlashCard { Content = newContent };

        _flashCardDataManager.UpdateFlashCardContent(oldFlashcard, newFlashCard, stack);
        Console.Clear();
    }

    public void DeleteFlashCard(Stack stack)
    {
        ViewFlashCardOfStack(stack);
        Console.Write("Enter name of FlashCard to delete or back to cancel: ");
        var name = _input.GetChoice();
        if (name.ToLower() == "back")
        {
            Console.Clear();
            return;
        }

        var flashCard = new FlashCard { Name = name };

        _flashCardDataManager.DeleteFlashCard(flashCard, stack);
        Console.Clear();
    }

    public void ViewFlashCardOfStack(Stack stack)
    {
        var stackList = _flashCardDataManager.GetFlashCardsOfStack(stack);
        _displayTable.DisplayTable(stackList, stack.Name);
    }

    public void CreateFlashCardTable()
    {
        _flashCardDataManager.CreateFlashCardTable();
    }
}
