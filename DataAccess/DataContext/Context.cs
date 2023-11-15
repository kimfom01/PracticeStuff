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
}