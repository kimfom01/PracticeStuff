using DataAccess.DataContext;
using DataAccess.Models;

namespace DataAccess.Repositories.Implementation;

public class StackRepository : RepositoryBase<Stack>, IStackRepository
{
    public StackRepository(Context context) : base(context)
    {
    }
}