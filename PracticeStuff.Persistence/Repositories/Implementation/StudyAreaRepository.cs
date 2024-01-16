using PracticeStuff.Core;
using PracticeStuff.Persistence.DataContext;

namespace PracticeStuff.Persistence.Repositories.Implementation;

public class StudyAreaRepository : RepositoryBase<StudyArea>, IStudyAreaRepository
{
    public StudyAreaRepository(Context context) : base(context)
    {
    }
}