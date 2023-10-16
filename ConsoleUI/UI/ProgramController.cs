using BusinessLogic.Enums;
using BusinessLogic.Services;
using Spectre.Console;

namespace ConsoleUI.UI;

public class ProgramController
{
    private readonly IStackService _stackService;
    private readonly IFlashCardService _flashCardService;
    private readonly IStudyAreaService _studyAreaService;

    public ProgramController(
        IFlashCardService flashCardService,
        IStudyAreaService studyAreaService,
        IStackService stackService)
    {
        _flashCardService = flashCardService;
        _studyAreaService = studyAreaService;
        _stackService = stackService;
    }

    private MainMenuOptions ViewMainMenu()
    {
        var choice = AnsiConsole.Prompt(new SelectionPrompt<MainMenuOptions>()
            .Title("Select an option ([grey]Move up and down and hit enter to select[/])")
            .AddChoices(
                MainMenuOptions.Study,
                MainMenuOptions.Settings,
                MainMenuOptions.Exit));

        return choice;
    }

    private void CreateTables()
    {
        _stackService.CreateStackTable();
        _flashCardService.CreateFlashCardTable();
        _studyAreaService.CreateStudyAreaTable();
    }

    public void StartProgram()
    {
        CreateTables();

        var choice = ViewMainMenu();

        while (choice != MainMenuOptions.Exit)
        {
            switch (choice)
            {
                case MainMenuOptions.Study:
                    _studyAreaService.ManageStudyArea();
                    break;
                case MainMenuOptions.Settings:
                    _stackService.ManageStacksSettings();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Wrong input!");
                    break;
            }

            choice = ViewMainMenu();
        }
    }
}