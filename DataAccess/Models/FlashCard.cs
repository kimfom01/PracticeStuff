namespace DataAccess.Models;

public class FlashCard
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Content { get; set; }
    
    public int StackId { get; set; }
    public Stack? Stack { get; set; }
}