using PracticeStuff.Application.Dtos.Stack;

namespace PracticeStuff.Application.Dtos.FlashCard;

public class GetFlashCardDetailDto : BaseDto
{
    public required string Front { get; set; }
    public required string Back { get; set; }
    public int StackId { get; set; }
    public GetStackListDto Stack { get; set; }
}