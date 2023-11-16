using DataAccess.DataContext;
using DataAccess.Models;

namespace DataAccess.Repositories.Implementation;

public class StudyAreaRepository : RepositoryBase<StudyArea>, IStudyAreaRepository
{
    public StudyAreaRepository(Context context) : base(context)
    {
    }
}