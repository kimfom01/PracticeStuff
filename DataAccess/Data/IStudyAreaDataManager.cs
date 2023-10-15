using DataAccess.DTO;
using DataAccess.Models;

namespace DataAccess.Data;

public interface IStudyAreaDataManager
{
    public void CreateStudyAreaTable();
    public void SaveScore(StudyArea studyArea, Stack stack);
    public List<StudyAreaDto> GetScoresHistory();
}
