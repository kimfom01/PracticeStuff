using PracticeStuff.Core;
using PracticeStuff.Persistence.DataContext;

namespace PracticeStuff.Persistence.Repositories.Implementation;

public class StackRepository : RepositoryBase<Stack>, IStackRepository
{
    public StackRepository(Context context) : base(context)
    {
    }
}