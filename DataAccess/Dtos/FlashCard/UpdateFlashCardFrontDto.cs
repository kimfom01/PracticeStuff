namespace DataAccess.Dtos.FlashCard;

public class UpdateFlashCardFrontDto
{
    public int Id { get; set; }
    public required string Front { get; set; }
    public int StackId { get; set; }
}