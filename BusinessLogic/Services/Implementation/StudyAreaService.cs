using BusinessLogic.Input;
using BusinessLogic.UI;
using DataAccess.Data;
using DataAccess.DTO;
using DataAccess.Models;

namespace BusinessLogic.Services.Implementation;

public class StudyAreaService : IStudyAreaService
{
    private readonly UserInput _input;
    private readonly TableVisualizationEngine _displayTable;
    private readonly IFlashCardDataManager _flashCardDataManager;
    private readonly IStudyAreaDataManager _studyAreaDataManager;

    public StudyAreaService(
        UserInput input,
        TableVisualizationEngine displayTable,
        IFlashCardDataManager flashCardDataManager,
        IStudyAreaDataManager studyAreaDataManager)
    {
        _input = input;
        _displayTable = displayTable;
        _flashCardDataManager = flashCardDataManager;
        _studyAreaDataManager = studyAreaDataManager;
    }

    public void ViewStudyAreaMenu()
    {
        Console.WriteLine("STUDY AREA\n");
        Console.WriteLine("new to Start a New Lesson");
        Console.WriteLine("history to View History");
        Console.WriteLine("back to Go Back");
        Console.WriteLine("\nType your choice and hit Enter");
        Console.Write("Your choice? ");
    }

    public void ManageStudyArea()
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

    public void ViewHistory()
    {
        _displayTable.ViewHistory();

        Console.WriteLine("Hit Enter to go back");
        Console.ReadLine();
        Console.Clear();
    }

    public void ViewNewLessonMenu()
    {
        Console.WriteLine("Type a Stack Name to choose or back to Go Back");
        Console.Write("Your choice? ");
    }

    public void StartLesson()
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

    public int PlayLessonLoop(List<FlashCardDTO> flashCards)
    {
        var score = 0;
        foreach (var card in flashCards)
        {
            Console.Clear();

            Console.WriteLine(card.Name);
            Console.Write("Your answer: ");
            var answer = _input.GetInput();

            if (answer.ToLower() != card?.Content?.ToLower())
            {
                Console.WriteLine("Incorrect!");
                Console.WriteLine("Correct answer is " + card?.Content);
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

    public void SaveScore(StudyArea studyArea, Stack stack)
    {
        _studyAreaDataManager.SaveScore(studyArea, stack);
    }

    public void CreateStudyAreaTable()
    {
        _studyAreaDataManager.CreateStudyAreaTable();
    }
}
