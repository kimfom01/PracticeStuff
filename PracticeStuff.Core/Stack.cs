using System.ComponentModel.DataAnnotations;
using PracticeStuff.Core.Common;

namespace PracticeStuff.Core;

public class Stack : BaseEntity
{
    [Required]
    public string? Name { get; set; }

    public IEnumerable<FlashCard>? FlashCards { get; set; }
    public IEnumerable<StudyArea>? StudyAreas { get; set; }
}