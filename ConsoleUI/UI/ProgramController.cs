using BusinessLogic.Input;
using BusinessLogic.Services;

namespace ConsoleUI.UI;

public class ProgramController
{
    private readonly IStackService _stackService;
    private readonly IFlashCardService _flashCardService;
    private readonly IStudyAreaService _studyAreaService;
    private readonly UserInput _input;

    public ProgramController(
        UserInput input,
        IFlashCardService flashCardService,
        IStudyAreaService studyAreaService,
        IStackService stackService)
    {
        _input = input;
        _flashCardService = flashCardService;
        _studyAreaService = studyAreaService;
        _stackService = stackService;
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
        _stackService.CreateStackTable();
        _flashCardService.CreateFlashCardTable();
        _studyAreaService.CreateStudyAreaTable();
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
                    _studyAreaService.ManageStudyArea();
                    break;
                case "settings":
                    _stackService.ManageStacksSettings();
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
}