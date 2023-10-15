using ConsoleTableExt;

namespace BusinessLogic.TableVisualizer;

public class VisualizationService<TModel> where TModel : class
{
    public void DisplayTable(List<TModel> list, string? tableTitle)
    {
        Console.Clear();

        ConsoleTableBuilder
            .From(list)
            .WithTitle(tableTitle)
            .ExportAndWriteLine();

        Console.WriteLine();
    }

    public void DisplayTable(List<TModel> list, string? columnName, string tableTitle)
    {
        Console.Clear();

        ConsoleTableBuilder
            .From(list)
            .WithColumn(columnName)
            .WithTitle(tableTitle)
            .ExportAndWriteLine();

        Console.WriteLine();
    }
}