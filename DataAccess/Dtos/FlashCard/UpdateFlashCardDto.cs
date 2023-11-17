namespace DataAccess.Dtos.FlashCard;

public class UpdateFlashCardDto : BaseDto
{
    public required string Front { get; set; }
    public required string Back { get; set; }
    public int StackId { get; set; }
}
