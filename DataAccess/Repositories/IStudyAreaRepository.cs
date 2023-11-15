using DataAccess.Dtos;
using DataAccess.Models;

namespace DataAccess.Repositories;

public interface IStudyAreaRepository
{
    public Task CreateStudyAreaTable();
    public Task SaveScore(StudyArea studyArea, Stack stack);
    public Task<IEnumerable<StudyAreaDto>> GetScoresHistory();
}
