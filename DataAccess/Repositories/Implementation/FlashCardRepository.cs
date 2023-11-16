using DataAccess.DataContext;
using DataAccess.Models;

namespace DataAccess.Repositories.Implementation;

public class FlashCardRepository : RepositoryBase<FlashCard>, IFlashCardRepository
{
    public FlashCardRepository(Context context) : base(context)
    {
    }
}