namespace DataAccess.Dtos.StudyArea;

public class CreateStudyAreaDto : BaseDto
{
    public DateTime Date { get; set; }
    public int Score { get; set; }
    public int StackId { get; set; }
}