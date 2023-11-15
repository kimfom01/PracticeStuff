using BusinessLogic.Enums;
using BusinessLogic.Input;
using BusinessLogic.TableVisualizer;
using DataAccess.Dtos;
using DataAccess.Models;
using DataAccess.Repositories;
using Spectre.Console;

namespace BusinessLogic.Services.Implementation;

public class StudyAreaService : IStudyAreaService
{
    private readonly UserInput _input;
    private readonly VisualizationService<StudyAreaDto> _studyAreaDisplayEngine;
    private readonly VisualizationService<StackDto> _stackDisplayEngine;
    private readonly IFlashCardRepository _flashCardRepository;
    private readonly IStudyAreaRepository _studyAreaRepository;
    private readonly IStackRepository _stackRepository;

    public StudyAreaService(
        UserInput input,
        VisualizationService<StudyAreaDto> studyAreaDisplayEngine,
        IFlashCardRepository flashCardRepository,
        IStudyAreaRepository studyAreaRepository,
        VisualizationService<StackDto> stackDisplayEngine,
        IStackRepository stackRepository)
    {
        _input = input;
        _studyAreaDisplayEngine = studyAreaDisplayEngine;
        _flashCardRepository = flashCardRepository;
        _studyAreaRepository = studyAreaRepository;
        _stackDisplayEngine = stackDisplayEngine;
        _stackRepository = stackRepository;
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

    public async Task ManageStudyArea()
    {
        Console.Clear();

        var choice = GetStudyAreaChoice();

        while (choice != StudyAreaOptions.Cancel)
        {
            switch (choice)
            {
                case StudyAreaOptions.New:
                    await StartLesson();
                    break;
                case StudyAreaOptions.History:
                    await ViewHistory();
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

    public async Task ViewHistory()
    {
        var historyList = await _studyAreaRepository.GetScoresHistory();
        _studyAreaDisplayEngine.DisplayTable(historyList.ToList(), "History");

        Console.WriteLine("Hit Enter to go back");
        Console.ReadLine();
        Console.Clear();
    }

    public void ViewNewLessonMenu()
    {
        Console.WriteLine("Type a Stack Name to choose or back to Go Back");
        Console.Write("Your choice? ");
    }

    public async Task StartLesson()
    {
        Console.Clear();

        var stackList = await _stackRepository.GetStacks();
        _stackDisplayEngine.DisplayTable(stackList.ToList(), "", "Lessons");

        ViewNewLessonMenu();
        var choice = _input.GetInput();

        if (choice == "back")
        {
            Console.Clear();
            return;
        }

        var stack = new Stack { Name = choice };
        var flashCards = await _flashCardRepository.GetFlashCardsOfStack(stack);

        var score = PlayLessonLoop(flashCards);

        Console.Clear();
        Console.WriteLine($"Your final score is: {score}");
        Console.WriteLine("Hit Enter to return to previous menu.");
        Console.ReadLine();

        var studyArea = new StudyArea { Score = score };
        await SaveScore(studyArea, stack);

        Console.Clear();
    }

    public int PlayLessonLoop(IEnumerable<FlashCardDto> flashCards)
    {
        var score = 0;
        foreach (var card in flashCards)
        {
            Console.Clear();

            Console.WriteLine(card.Name);
            Console.Write("Your answer: ");
            var answer = _input.GetInput();

            if (answer.ToLower() != card.Content?.ToLower())
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

    public async Task SaveScore(StudyArea studyArea, Stack stack)
    {
        await _studyAreaRepository.SaveScore(studyArea, stack);
    }

    public async Task CreateStudyAreaTable()
    {
        await _studyAreaRepository.CreateStudyAreaTable();
    }
}