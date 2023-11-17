using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models;

public class FlashCard
{
    public int Id { get; set; }
    
    [Required]
    public string? Front { get; set; }
    
    [Required]
    public string? Back { get; set; }
    
    public int StackId { get; set; }
    public Stack? Stack { get; set; }
}