using PracticeStuff.Application.Dtos.StudyArea;

namespace PracticeStuff.Application.Services;

public interface IStudyAreaService
{
    Task<CreateStudyAreaDto?> AddStudyArea(CreateStudyAreaDto createStudyAreaDto);
    Task<int> UpdateStudyArea(UpdateStudyAreaDto updateStudyAreaDto);
    Task<int> DeleteStudyArea(int id);
    Task<IEnumerable<GetStudyAreaListDto>> GetStudyAreas();
    Task<GetStudyAreaDetailDto?> GetStudyArea(int id);
}
