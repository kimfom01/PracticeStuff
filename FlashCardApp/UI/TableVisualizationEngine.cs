using ConsoleTableExt;
using FlashCardApp.Data;
using FlashCardApp.Models;

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

    public void ViewFlashCards(Stack stack)
    {
        Console.Clear();
        
        ConsoleTableBuilder.From(DbManager.GetFlashCardsOfStack(stack)).ExportAndWriteLine();
        Console.WriteLine();
    }
}