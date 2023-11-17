namespace DataAccess.Dtos.FlashCard;

public class UpdateFlashCardBackDto : BaseDto
{
    public required string Back { get; set; }
    public int StackId { get; set; }
}