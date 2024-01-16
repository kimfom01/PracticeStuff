using Microsoft.EntityFrameworkCore;
using PracticeStuff.Core;
using PracticeStuff.Persistence.DataContext;

namespace PracticeStuff.Persistence.Repositories.Implementation;

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

    public async Task<FlashCard?> GetFlashCard(int stackId, int id)
    {
        var flashCard = await DbEntitySet.Where(fl => fl.Id == id && fl.StackId == stackId)
            .Include(fl => fl.Stack)
            .FirstOrDefaultAsync();

        return flashCard;
    }
}