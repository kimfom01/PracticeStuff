namespace FlashCardApp.Models;

public class FlashCard
{
    public int Id { get; set; }
    public int StackId { get; set; }
    public string FlashCardName { get; set; }
    public string FrontContent { get; set; }
    public string BackContent { get; set; }
}