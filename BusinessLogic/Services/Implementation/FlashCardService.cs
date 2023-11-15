using BusinessLogic.Enums;
using BusinessLogic.Input;
using BusinessLogic.TableVisualizer;
using DataAccess.Dtos;
using DataAccess.Models;
using DataAccess.Repositories;
using Spectre.Console;

namespace BusinessLogic.Services.Implementation;

public class FlashCardService : IFlashCardService
{
    private readonly UserInput _input;
    private readonly IFlashCardRepository _flashCardRepository;
    private readonly VisualizationService<FlashCardDto> _displayTable;

    public FlashCardService(
        UserInput input,
        VisualizationService<FlashCardDto> displayTable,
        IFlashCardRepository flashCardRepository)
    {
        _input = input;
        _displayTable = displayTable;
        _flashCardRepository = flashCardRepository;
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

    public async Task ManageFlashCardSettings(Stack stack)
    {
        Console.Clear();
        var choice = GetFlashCardSettingsChoice();

        while (choice != FlashCardSettingsOptions.Cancel)
        {
            switch (choice)
            {
                case FlashCardSettingsOptions.View:
                    await ViewFlashCards(stack);
                    break;
                case FlashCardSettingsOptions.Add:
                    await AddFlashCardToStack(stack);
                    break;
                case FlashCardSettingsOptions.Edit:
                    await EditFlashCard(stack);
                    break;
                case FlashCardSettingsOptions.Delete:
                    await DeleteFlashCard(stack);
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

    public async Task ViewFlashCards(Stack stack)
    {
        Console.Clear();

        await ViewFlashCardOfStack(stack);

        Console.WriteLine("Hit Enter to return to previous menu.");
        Console.ReadLine();
        Console.Clear();
    }

    public async Task AddFlashCardToStack(Stack stack)
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

        await _flashCardRepository.AddNewFlashCard(flashcard, stack);
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

    public async Task EditFlashCard(Stack stack)
    {
        Console.Clear();

        var choice = GetEditFlashCardChoice();

        while (choice != EditFlashCardOptions.Cancel)
        {
            switch (choice)
            {
                case EditFlashCardOptions.All:
                    await EditAll(stack);
                    break;
                case EditFlashCardOptions.Front:
                    await EditFlashCardName(stack);
                    break;
                case EditFlashCardOptions.Back:
                    await EditBack(stack);
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

    public async Task EditAll(Stack stack)
    {
        await ViewFlashCardOfStack(stack);
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

        await _flashCardRepository.UpdateFlashCard(oldFlashcard, newFlashCard, stack);
        Console.Clear();
    }

    public async Task EditFlashCardName(Stack stack)
    {
        await ViewFlashCardOfStack(stack);
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

        await _flashCardRepository.UpdateFlashCardName(oldFlashcard, newFlashCard, stack);
        Console.Clear();
    }

    public async Task EditBack(Stack stack)
    {
        await ViewFlashCardOfStack(stack);
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

        await _flashCardRepository.UpdateFlashCardContent(oldFlashcard, newFlashCard, stack);
        Console.Clear();
    }

    public async Task DeleteFlashCard(Stack stack)
    {
        await ViewFlashCardOfStack(stack);
        Console.Write("Enter name of FlashCard to delete or back to cancel: ");
        var name = _input.GetChoice();
        if (name.ToLower() == "back")
        {
            Console.Clear();
            return;
        }

        var flashCard = new FlashCard { Name = name };

        await _flashCardRepository.DeleteFlashCard(flashCard, stack);
        Console.Clear();
    }

    public async Task ViewFlashCardOfStack(Stack stack)
    {
        var stackList = await _flashCardRepository.GetFlashCardsOfStack(stack);
        _displayTable.DisplayTable(stackList.ToList(), stack.Name);
    }

    public async Task CreateFlashCardTable()
    {
        await _flashCardRepository.CreateFlashCardTable();
    }
}