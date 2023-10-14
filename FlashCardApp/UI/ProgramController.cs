using FlashCardApp.Data;
using FlashCardApp.DTO;
using FlashCardApp.Input;
using FlashCardApp.Models;

namespace FlashCardApp.UI;

public class ProgramController
{
    private readonly IStackDataManager _stackDataManager;
    private readonly IFlashCardDataManager _flashCardDataManager;
    private readonly IStudyAreaDataManager _studyAreaDataManager;
    private readonly UserInput _input;
    private readonly TableVisualizationEngine _displayTable;

    public ProgramController(
        IStackDataManager stackDataManager,
        IFlashCardDataManager flashCardDataManager,
        IStudyAreaDataManager studyAreaDataManager,
        UserInput input,
        TableVisualizationEngine displayTable)
    {
        _stackDataManager = stackDataManager;
        _flashCardDataManager = flashCardDataManager;
        _studyAreaDataManager = studyAreaDataManager;
        _input = input;
        _displayTable = displayTable;
    }

    private void ViewMainMenu()
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

    private void CreateTables()
    {
        _stackDataManager.CreateStackTable();
        _flashCardDataManager.CreateFlashCardTable();
        _studyAreaDataManager.CreateStudyAreaTable();
    }

    public void StartProgram()
    {
        CreateTables();

        ViewMainMenu();
        var choice = _input.GetChoice();

        while (choice != "exit")
        {
            switch (choice)
            {
                case "study":
                    ManageStudyArea();
                    break;
                case "settings":
                    ManageStacksSettings();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Wrong input!");
                    break;
            }

            ViewMainMenu();
            choice = _input.GetChoice();
        }
    }

    // Study Area Operations
    private static void ViewStudyAreaMenu()
    {
        Console.WriteLine("STUDY AREA\n");
        Console.WriteLine("new to Start a New Lesson");
        Console.WriteLine("history to View History");
        Console.WriteLine("back to Go Back");
        Console.WriteLine("\nType your choice and hit Enter");
        Console.Write("Your choice? ");
    }

    private void ManageStudyArea()
    {
        Console.Clear();

        ViewStudyAreaMenu();
        var choice = _input.GetChoice();

        while (choice != "back")
        {
            switch (choice)
            {
                case "new":
                    StartLesson();
                    break;
                case "history":
                    ViewHistory();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Wrong input!");
                    break;
            }

            ViewStudyAreaMenu();
            choice = _input.GetChoice();
        }

        Console.Clear();
    }

    private void ViewHistory()
    {
        _displayTable.ViewHistory();

        Console.WriteLine("Hit Enter to go back");
        Console.ReadLine();
        Console.Clear();
    }

    private static void ViewNewLessonMenu()
    {
        Console.WriteLine("Type a Stack Name to choose or back to Go Back");
        Console.Write("Your choice? ");
    }

    private void StartLesson()
    {
        Console.Clear();

        _displayTable.ViewStacks();

        ViewNewLessonMenu();
        var choice = _input.GetInput();
        if (choice == "back")
        {
            Console.Clear();
            return;
        }

        var stack = new Stack { Name = choice };
        var flashCards = _flashCardDataManager.GetFlashCardsOfStack(stack);

        var score = PlayLessonLoop(flashCards);

        Console.Clear();
        Console.WriteLine($"Your final score is: {score}");
        Console.WriteLine("Hit Enter to return to previous menu.");
        Console.ReadLine();

        var studyArea = new StudyArea { Score = score };
        SaveScore(studyArea, stack);

        Console.Clear();
    }

    private int PlayLessonLoop(List<FlashCardDTO> flashCards)
    {
        var score = 0;
        foreach (var card in flashCards)
        {
            Console.Clear();

            Console.WriteLine(card.Name);
            Console.Write("Your answer: ");
            var answer = _input.GetInput();

            if (answer.ToLower() != card.Content.ToLower())
            {
                Console.WriteLine("Incorrect!");
                Console.WriteLine("Correct answer is " + card.Content);
                Console.Write("Press any key to continue...");
                Console.ReadLine();
                continue;
            }

            Console.WriteLine("Correct");
            Console.Write("Press any key to continue...");
            Console.ReadLine();
            score++;
        }

        return score;
    }

    private void SaveScore(StudyArea studyArea, Stack stack)
    {
        _studyAreaDataManager.SaveScore(studyArea, stack);
    }

    // Settings
    private void DisplaySettingsMenu()
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

    private void ManageStacksSettings()
    {
        Console.Clear();

        DisplaySettingsMenu();
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

            DisplaySettingsMenu();
            choice = _input.GetChoice();
        }

        Console.Clear();
    }

    // Stack Operations
    private void GetStackToAdd()
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

    private static void DisplayUpdateStackMenu()
    {
        Console.WriteLine("type the name of stack you want to rename");
        Console.WriteLine("back to Go Back");
        Console.Write("\nYour choice? ");
    }

    private void UpdateStackName()
    {
        _displayTable.ViewStacks();
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

            _displayTable.ViewStacks();
            DisplayUpdateStackMenu();
            choice = _input.GetChoice();
        }

        Console.Clear();
    }

    private static void DisplayDeleteMenu()
    {
        Console.WriteLine("type the name of stack you want to delete");
        Console.WriteLine("back to Go Back");
        Console.Write("\nYour choice? ");
    }

    private void DeleteStack()
    {
        _displayTable.ViewStacks();
        DisplayDeleteMenu();
        var choice = _input.GetChoice();

        while (choice != "back")
        {
            _stackDataManager.DeleteStack(new Stack { Name = choice });

            _displayTable.ViewStacks();
            DisplayDeleteMenu();
            choice = _input.GetChoice();
        }

        Console.Clear();
    }

    private void ViewStackForFlashCardOperations()
    {
        Console.Clear();
        _displayTable.ViewStacks();

        SelectStackToOperateOn();
        Console.Clear();
    }

    private void SelectStackToOperateOn()
    {
        Console.WriteLine("Type Stack Name and hit Enter to Perform Operations on a Stack: ");
        Console.WriteLine("back to Go Back");
        var name = _input.GetInput();
        if (name == "back")
        {
            Console.Clear();
            return;
        }

        ManageFlashCardSettings(new Stack { Name = name });
    }

    // FlashCard Operations

    private void ViewFlashCardSettingsMenu()
    {
        Console.WriteLine("view to View FlashCards of the Stack");
        Console.WriteLine("add to Add a New FlashCard");
        Console.WriteLine("edit to Edit a FlashCard");
        Console.WriteLine("delete to Delete a FlashCard");
        Console.WriteLine("back to Go Back");
        Console.WriteLine("\nType your choice and hit Enter");
        Console.Write("Your choice? ");
    }

    private void ManageFlashCardSettings(Stack stack)
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

    private void ViewFlashCards(Stack stack)
    {
        Console.Clear();

        ViewFlashCardOfStack(stack);

        Console.WriteLine("Hit Enter to return to previous menu.");
        Console.ReadLine();
        Console.Clear();
    }

    private void AddFlashCardToStack(Stack stack)
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

    private void ViewEditFlashCardMenu()
    {
        Console.WriteLine("all to Edit Front and Back");
        Console.WriteLine("edit front to Edit Front");
        Console.WriteLine("edit back to Edit Back");
        Console.WriteLine("back to Go Back");
        Console.WriteLine("\nType your choice and hit Enter");
        Console.Write("Your choice? ");
    }

    private void EditFlashCard(Stack stack)
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

    private void EditAll(Stack stack)
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

    private void EditFlashCardName(Stack stack)
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

    private void EditBack(Stack stack)
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

    private void DeleteFlashCard(Stack stack)
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

    private void ViewFlashCardOfStack(Stack stack)
    {
        _displayTable.ViewFlashCards(stack);
    }
}