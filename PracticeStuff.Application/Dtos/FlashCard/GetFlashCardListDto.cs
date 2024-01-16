namespace PracticeStuff.Application.Dtos.FlashCard;

public class GetFlashCardListDto : BaseDto
{
    public required string Front { get; set; }
    public required string Back { get; set; }
    public int StackId { get; set; }
}