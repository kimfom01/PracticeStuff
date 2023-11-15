using DataAccess.DataContext;

namespace DataAccess.Repositories.Implementation;

public class UnitOfWork : IUnitOfWork
{
    private readonly Context _context;

    public UnitOfWork(Context context)
    {
        _context = context;
        FlashCards = new FlashCardRepository(_context);
        StudyAreas = new StudyAreaRepository(_context);
        Stacks = new StackRepository(_context);
    }

    public IFlashCardRepository FlashCards { get; set; }
    public IStudyAreaRepository StudyAreas { get; set; }
    public IStackRepository Stacks { get; set; }
    
    public async Task<int> SaveChanges()
    {
        return await _context.SaveChangesAsync();
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }
}