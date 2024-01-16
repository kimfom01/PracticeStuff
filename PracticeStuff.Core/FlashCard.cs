using System.ComponentModel.DataAnnotations;
using PracticeStuff.Core.Common;

namespace PracticeStuff.Core;

public class FlashCard : BaseEntity
{
    [Required]
    public string? Front { get; set; }
    
    [Required]
    public string? Back { get; set; }
    
    public int StackId { get; set; }
    public Stack? Stack { get; set; }
}