using ConsoleTableExt;
using FlashCardApp.Data;

namespace FlashCardApp.UI;

public class TableVisualizationEngine
{
    private static readonly DatabaseManager DbManager = new();
    public void ViewStacks()
    {
        Console.Clear();
        
        ConsoleTableBuilder.From(DbManager.GetStacks()).ExportAndWriteLine();
        Console.WriteLine();
    }

    public void ViewFlashCards()
    {
        Console.Clear();
        
        ConsoleTableBuilder.From(DbManager.GetFlashAllCards()).ExportAndWriteLine();
        Console.WriteLine();
    }
}