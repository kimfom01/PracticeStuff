namespace DataAccess.Repositories;

public interface IUnitOfWork : IDisposable
{
    public IFlashCardRepository FlashCards { get; set; }
    public IStudyAreaRepository StudyAreas { get; set; }
    public IStackRepository Stacks { get; set; }
    public Task<int> SaveChanges();
}