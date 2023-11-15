namespace DataAccess.Models;

public class Stack
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public IEnumerable<FlashCard>? FlashCards { get; set; }
    public IEnumerable<StudyArea>? StudyAreas { get; set; }
}