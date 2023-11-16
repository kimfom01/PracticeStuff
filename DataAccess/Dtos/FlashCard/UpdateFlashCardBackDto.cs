namespace DataAccess.Dtos.FlashCard;

public class UpdateFlashCardBackDto
{
    public int Id { get; set; }
    public required string Back { get; set; }
    public int StackId { get; set; }
}