// See https://aka.ms/new-console-template for more information

using FlashCardApp.Data;

DBManager.CreateStackTable();
DBManager.CreateFlashCardTable();
DBManager.CreateStudyAreaTable();

DBManager.DeleteStack("States");

DBManager.AddNewStack("Capitals");

PrintStacks();

DBManager.UpdateStack("Capitals", "States");

PrintStacks();

void PrintStacks()
{
    var stacks = DBManager.GetStacks();

    foreach (var stack in stacks)
    {
        Console.WriteLine(stack.StackName);
    }
}