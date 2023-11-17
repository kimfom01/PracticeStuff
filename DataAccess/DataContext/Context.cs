using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DataContext;

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
        modelBuilder.Entity<Stack>()
            .HasData(
                new Stack
                {
                    Id = 1,
                    Name = "Mathematics"
                },
                new Stack
                {
                    Id = 2,
                    Name = "Physics"
                },
                new Stack
                {
                    Id = 3,
                    Name = "Chemistry"
                },
                new Stack
                {
                    Id = 4,
                    Name = "History"
                },
                new Stack
                {
                    Id = 5,
                    Name = "Geography"
                },
                new Stack
                {
                    Id = 6,
                    Name = "Biology"
                },
                new Stack
                {
                    Id = 7,
                    Name = "English Literature"
                },
                new Stack
                {
                    Id = 8,
                    Name = "Computer Science"
                },
                new Stack
                {
                    Id = 9,
                    Name = "Art"
                },
                new Stack
                {
                    Id = 10,
                    Name = "Music Theory"
                }
            );

        modelBuilder.Entity<FlashCard>()
            .HasData(
                new FlashCard
                {
                    Id = 1,
                    Front = "Calculus Basics",
                    Back = "Introduction to calculus concepts",
                    StackId = 1
                },
                new FlashCard
                {
                    Id = 2,
                    Front = "Newton's Laws",
                    Back = "Principles of motion and force",
                    StackId = 2
                },
                new FlashCard
                {
                    Id = 3,
                    Front = "Organic Chemistry",
                    Back = "Fundamentals of organic compounds",
                    StackId = 3
                },
                new FlashCard
                {
                    Id = 4,
                    Front = "World War II",
                    Back = "Key events of WWII",
                    StackId = 4
                },
                new FlashCard
                {
                    Id = 5,
                    Front = "Continents and Oceans",
                    Back = "Geographic features of Earth",
                    StackId = 5
                },
                new FlashCard
                {
                    Id = 6,
                    Front = "Human Anatomy",
                    Back = "Structure of the human body",
                    StackId = 6
                },
                new FlashCard
                {
                    Id = 7,
                    Front = "Shakespeare's Plays",
                    Back = "Overview of Shakespearean drama",
                    StackId = 7
                },
                new FlashCard
                {
                    Id = 8,
                    Front = "Programming Basics",
                    Back = "Introduction to programming languages",
                    StackId = 8
                },
                new FlashCard
                {
                    Id = 9,
                    Front = "Art Movements",
                    Back = "History of different art styles",
                    StackId = 9
                },
                new FlashCard
                {
                    Id = 10,
                    Front = "Music Composition",
                    Back = "Basics of composing music",
                    StackId = 10
                }
            );

        modelBuilder.Entity<StudyArea>()
            .HasData(
                new StudyArea
                {
                    Id = 1,
                    Date = DateTime.Now,
                    Score = 85,
                    StackId = 1
                },
                new StudyArea
                {
                    Id = 2,
                    Date = DateTime.Now.AddDays(-1),
                    Score = 90,
                    StackId = 2
                },
                new StudyArea
                {
                    Id = 3,
                    Date = DateTime.Now.AddDays(-2),
                    Score = 75,
                    StackId = 3
                },
                new StudyArea
                {
                    Id = 4,
                    Date = DateTime.Now.AddDays(-3),
                    Score = 80,
                    StackId = 4
                },
                new StudyArea
                {
                    Id = 5,
                    Date = DateTime.Now.AddDays(-4),
                    Score = 95,
                    StackId = 5
                },
                new StudyArea
                {
                    Id = 6,
                    Date = DateTime.Now.AddDays(-5),
                    Score = 70,
                    StackId = 6
                },
                new StudyArea
                {
                    Id = 7,
                    Date = DateTime.Now.AddDays(-6),
                    Score = 88,
                    StackId = 7
                },
                new StudyArea
                {
                    Id = 8,
                    Date = DateTime.Now.AddDays(-7),
                    Score = 82,
                    StackId = 8
                },
                new StudyArea
                {
                    Id = 9,
                    Date = DateTime.Now.AddDays(-8),
                    Score = 77,
                    StackId = 9
                },
                new StudyArea
                {
                    Id = 10,
                    Date = DateTime.Now.AddDays(-9),
                    Score = 91,
                    StackId = 10
                }
            );
    }
}