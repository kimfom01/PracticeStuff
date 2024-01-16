using PracticeStuff.Core.Common;

namespace PracticeStuff.Core;

public class StudyArea : BaseEntity
{
    public DateTime Date { get; set; }
    public int Score { get; set; }
    
    public int StackId { get; set; }
    public Stack? Stack { get; set; }
}