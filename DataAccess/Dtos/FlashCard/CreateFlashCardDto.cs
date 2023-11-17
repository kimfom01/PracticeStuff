namespace DataAccess.Dtos.FlashCard;

public class CreateFlashCardDto : BaseDto
{
    public required string Front { get; set; }
    public required string Back { get; set; }
    public int StackId { get; set; }
}