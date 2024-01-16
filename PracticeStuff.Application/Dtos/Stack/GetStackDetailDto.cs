using PracticeStuff.Application.Dtos.FlashCard;
using PracticeStuff.Application.Dtos.StudyArea;

namespace PracticeStuff.Application.Dtos.Stack;

public class GetStackDetailDto : BaseDto
{
    public required string Name { get; set; }
    public IEnumerable<GetFlashCardListDto>? FlashCards { get; set; }
    public IEnumerable<GetStudyAreaListDto>? StudyAreas { get; set; }
}