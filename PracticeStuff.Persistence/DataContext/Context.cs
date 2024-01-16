using PracticeStuff.Core;
using Microsoft.EntityFrameworkCore;

namespace PracticeStuff.Persistence.DataContext;

public class Context : DbContext
{
    public DbSet<FlashCard> FlashCards { get; set; }
    public DbSet<Stack> Stacks { get; set; }
    public DbSet<StudyArea> StudyAreas { get; set; }

    public Context(DbContextOptions<Context> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Stack>().HasData(new List<Stack>
        {
            new() { Id = 1, Name = "Science Fundamentals" },
            new() { Id = 2, Name = "World History" },
            new() { Id = 3, Name = "Spanish Language" }
        });

        modelBuilder.Entity<FlashCard>().HasData(new List<FlashCard>
        {
            new() { Id = 1, Front = "What is the chemical formula for water?", Back = "H2O", StackId = 1 },
            new() { Id = 2, Front = "What is Newton's Second Law of Motion?", Back = "Force equals mass times acceleration (F=ma)", StackId = 1 },
            new() { Id = 3, Front = "Who was the first Emperor of Rome?", Back = "Augustus", StackId = 2 },
            new() { Id = 4, Front = "What year did the French Revolution start?", Back = "1789", StackId = 2 },
            new() { Id = 5, Front = "How do you say 'Thank you' in Spanish?", Back = "Gracias", StackId = 3 },
            new() { Id = 6, Front = "What is the Spanish word for 'library'?", Back = "Biblioteca", StackId = 3 }
        });

        modelBuilder.Entity<StudyArea>().HasData(new List<StudyArea>
        {
            new() { Id = 1, Date = DateTime.Now, Score = 85, StackId = 1 },
            new() { Id = 2, Date = DateTime.Now.AddDays(-1), Score = 78, StackId = 1 },
            new() { Id = 3, Date = DateTime.Now, Score = 82, StackId = 2 },
            new() { Id = 4, Date = DateTime.Now.AddDays(-1), Score = 76, StackId = 2 },
            new() { Id = 5, Date = DateTime.Now, Score = 90, StackId = 3 },
            new() { Id = 6, Date = DateTime.Now.AddDays(-1), Score = 88, StackId = 3 }
        });
    }
}