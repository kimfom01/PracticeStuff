using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models;

public class Stack
{
    public int Id { get; set; }
    
    [Required]
    public string? Name { get; set; }

    public IEnumerable<FlashCard>? FlashCards { get; set; }
    public IEnumerable<StudyArea>? StudyAreas { get; set; }
}