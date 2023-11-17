using DataAccess.Dtos.FlashCard;
using DataAccess.Dtos.StudyArea;

namespace DataAccess.Dtos.Stack;

public class GetStackDetailDto : BaseDto
{
    public required string Name { get; set; }
    public IEnumerable<GetFlashCardListDto>? FlashCards { get; set; }
    public IEnumerable<GetStudyAreaListDto>? StudyAreas { get; set; }
}