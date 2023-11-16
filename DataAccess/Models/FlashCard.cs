namespace DataAccess.Models;

public class FlashCard
{
    public int Id { get; set; }
    public required string Front { get; set; }
    public required string Back { get; set; }
    
    public int StackId { get; set; }
    public Stack? Stack { get; set; }
}