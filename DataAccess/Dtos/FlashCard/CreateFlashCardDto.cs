namespace DataAccess.Dtos.FlashCard;

public class CreateFlashCardDto
{
    public string? Front { get; set; }
    public string? Back { get; set; }
    public int StackId { get; set; }
}