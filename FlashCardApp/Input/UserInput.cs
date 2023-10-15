namespace FlashCardApp.Input;

public class UserInput
{
    public string GetChoice()
    {
        string? choice = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(choice))
        {
            choice = Console.ReadLine();
        }
        return choice.Trim().ToLower();
    }

    public string GetInput()
    {
        string? input = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(input))
        {
            input = Console.ReadLine();
        }
        return input.Trim().ToLower();
    }
}