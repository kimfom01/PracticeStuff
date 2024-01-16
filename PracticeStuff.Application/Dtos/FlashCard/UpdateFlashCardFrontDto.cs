namespace PracticeStuff.Application.Dtos.FlashCard;

public class UpdateFlashCardFrontDto : BaseDto
{
    public required string Front { get; set; }
    public int StackId { get; set; }
}