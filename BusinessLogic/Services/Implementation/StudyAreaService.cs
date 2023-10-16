using BusinessLogic.Enums;
using BusinessLogic.Input;
using BusinessLogic.TableVisualizer;
using DataAccess.Data;
using DataAccess.DTO;
using DataAccess.Models;
using Spectre.Console;

namespace BusinessLogic.Services.Implementation;

public class StudyAreaService : IStudyAreaService
{
    private readonly UserInput _input;
    private readonly VisualizationService<StudyAreaDto> _studyAreaDisplayEngine;
    private readonly VisualizationService<StackDTO> _stackDisplayEngine;
    private readonly IFlashCardDataManager _flashCardDataManager;
    private readonly IStudyAreaDataManager _studyAreaDataManager;
    private readonly IStackDataManager _stackDataManager;

    public StudyAreaService(
        UserInput input,
        VisualizationService<StudyAreaDto> studyAreaDisplayEngine,
        IFlashCardDataManager flashCardDataManager,
        IStudyAreaDataManager studyAreaDataManager,
        VisualizationService<StackDTO> stackDisplayEngine,
        IStackDataManager stackDataManager)
    {
        _input = input;
        _studyAreaDisplayEngine = studyAreaDisplayEngine;
        _flashCardDataManager = flashCardDataManager;
        _studyAreaDataManager = studyAreaDataManager;
        _stackDisplayEngine = stackDisplayEngine;
        _stackDataManager = stackDataManager;
    }

    public StudyAreaOptions GetStudyAreaChoice()
    {
        var choice = AnsiConsole.Prompt(new SelectionPrompt<StudyAreaOptions>()
            .Title("Select an option ([grey]Move up and down and hit enter to select[/])")
            .AddChoices(
                StudyAreaOptions.New,
                StudyAreaOptions.History,
                StudyAreaOptions.Cancel));

        return choice;
    }

    public void ManageStudyArea()
    {
        Console.Clear();

        var choice = GetStudyAreaChoice();

        while (choice != StudyAreaOptions.Cancel)
        {
            switch (choice)
            {
                case StudyAreaOptions.New:
                    StartLesson();
                    break;
                case StudyAreaOptions.History:
                    ViewHistory();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Wrong input!");
                    break;
            }

            choice = GetStudyAreaChoice();
        }

        Console.Clear();
    }

    public void ViewHistory()
    {
        var historyList = _studyAreaDataManager.GetScoresHistory();
        _studyAreaDisplayEngine.DisplayTable(historyList, "History");

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

        var stackList = _stackDataManager.GetStacks();
        _stackDisplayEngine.DisplayTable(stackList, "", "Lessons");

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
