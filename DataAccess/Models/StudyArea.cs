namespace DataAccess.Models;

public class StudyArea
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public int Score { get; set; }
    
    public int StackId { get; set; }
    public Stack? Stack { get; set; }
}