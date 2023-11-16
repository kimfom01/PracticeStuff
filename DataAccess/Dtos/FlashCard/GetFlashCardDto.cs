namespace DataAccess.Dtos.FlashCard;

public class GetFlashCardDto
{
    public int Id { get; set; }
    public required string Front { get; set; }
    public required string Back { get; set; }
    public int StackId { get; set; }
}
