using DataAccess.DataContext;
using DataAccess.Models;

namespace DataAccess.Repositories.Implementation;

public class FlashCardRepository : RepositoryBase<FlashCard>, IFlashCardRepository
{
    public FlashCardRepository(Context context) : base(context)
    {
    }

    public async Task UpdateFlashCardFront(FlashCard flashCard)
    {
        DbEntitySet.Attach(flashCard);

        await Task.FromResult(DbEntitySet.Entry(flashCard)
            .Property(nameof(flashCard.Front)).IsModified = true);
    }

    public async Task UpdateFlashCardBack(FlashCard flashCard)
    {
        DbEntitySet.Attach(flashCard);

        await Task.FromResult(DbEntitySet.Entry(flashCard)
            .Property(nameof(flashCard.Back)).IsModified = true);
    }
}