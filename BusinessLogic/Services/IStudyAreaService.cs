using DataAccess.Models;

namespace BusinessLogic.Services;

public interface IStudyAreaService
{
    Task<StudyArea?>  AddStudyArea(StudyArea studyArea);
    Task<int> UpdateStudyArea(StudyArea studyArea);
    Task<int> DeleteStudyArea(int id);
    Task<IEnumerable<StudyArea>> GetStudyAreas();
    Task<StudyArea?> GetStudyArea(int id);
}
