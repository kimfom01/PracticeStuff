using DataAccess.Dtos.Stack;

namespace DataAccess.Dtos.StudyArea;

public class GetStudyAreaDetailDto : BaseDto
{
    public DateTime Date { get; set; }
    public int Score { get; set; }
    
    public int StackId { get; set; }
    public GetStackListDto? Stack { get; set; }
}