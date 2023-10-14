using ConsoleTableExt;
using FlashCardApp.Data;
using FlashCardApp.Models;

namespace FlashCardApp.UI;

public class TableVisualizationEngine
{
    private readonly IFlashCardDataManager _flashCardDataManager;
    private readonly IStackDataManager _stackDataManager;
    private readonly IStudyAreaDataManager _studyAreaDataManager;

    public TableVisualizationEngine(
        IFlashCardDataManager flashCardDataManager,
        IStackDataManager stackDataManager,
        IStudyAreaDataManager studyAreaDataManager)
    {
        _flashCardDataManager = flashCardDataManager;
        _stackDataManager = stackDataManager;
        _studyAreaDataManager = studyAreaDataManager;
    }
    public void ViewStacks()
    {
        Console.Clear();

        ConsoleTableBuilder
            .From(_stackDataManager.GetStacks())
            .WithColumn("Lessons")
            .ExportAndWriteLine();

        Console.WriteLine();
    }

    public void ViewFlashCards(Stack stack)
    {
        Console.Clear();

        ConsoleTableBuilder
            .From(_flashCardDataManager.GetFlashCardsOfStack(stack))
            .WithTitle(stack.Name)
            .ExportAndWriteLine();

        Console.WriteLine();
    }

    public void ViewHistory()
    {
        Console.Clear();

        ConsoleTableBuilder.From(_studyAreaDataManager.GetScoresHistory()).ExportAndWriteLine();
        Console.WriteLine();
    }
}