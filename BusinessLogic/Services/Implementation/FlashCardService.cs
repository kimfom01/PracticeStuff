using BusinessLogic.Input;
using BusinessLogic.UI;
using DataAccess.Data;
using DataAccess.Models;

namespace BusinessLogic.Services.Implementation;

public class FlashCardService : IFlashCardService
{
    private readonly UserInput _input;
    private readonly IFlashCardDataManager _flashCardDataManager;
    private readonly TableVisualizationEngine _displayTable;

    public FlashCardService(
        UserInput input,
    TableVisualizationEngine displayTable,
    IFlashCardDataManager flashCardDataManager)
    {
        _input = input;
        _displayTable = displayTable;
        _flashCardDataManager = flashCardDataManager;
    }

    public void ViewFlashCardSettingsMenu()
    {
        Console.WriteLine("view to View FlashCards of the Stack");
        Console.WriteLine("add to Add a New FlashCard");
        Console.WriteLine("edit to Edit a FlashCard");
        Console.WriteLine("delete to Delete a FlashCard");
        Console.WriteLine("back to Go Back");
        Console.WriteLine("\nType your choice and hit Enter");
        Console.Write("Your choice? ");
    }

    public void ManageFlashCardSettings(Stack stack)
    {
        Console.Clear();
        ViewFlashCardSettingsMenu();
        var choice = _input.GetChoice();

        while (choice != "back")
        {
            switch (choice)
            {
                case "view":
                    ViewFlashCards(stack);
                    break;
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

            ViewFlashCardSettingsMenu();
            choice = _input.GetChoice();
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

    public void ViewEditFlashCardMenu()
    {
        Console.WriteLine("all to Edit Front and Back");
        Console.WriteLine("edit front to Edit Front");
        Console.WriteLine("edit back to Edit Back");
        Console.WriteLine("back to Go Back");
        Console.WriteLine("\nType your choice and hit Enter");
        Console.Write("Your choice? ");
    }

    public void EditFlashCard(Stack stack)
    {
        Console.Clear();
        ViewEditFlashCardMenu();
        var choice = _input.GetChoice();

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

            ViewEditFlashCardMenu();
            choice = _input.GetChoice();
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
        _displayTable.ViewFlashCards(stack);
    }

    public void CreateFlashCardTable()
    {
        _flashCardDataManager.CreateFlashCardTable();
    }
}
