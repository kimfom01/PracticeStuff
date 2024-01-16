using AutoMapper;
using PracticeStuff.Application.Dtos.StudyArea;
using PracticeStuff.Core;
using PracticeStuff.Persistence.Repositories;

namespace PracticeStuff.Application.Services.Implementation;

public class StudyAreaService : IStudyAreaService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public StudyAreaService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CreateStudyAreaDto?> AddStudyArea(CreateStudyAreaDto createStudyAreaDto)
    {
        var studyArea = _mapper.Map<StudyArea>(createStudyAreaDto);
        
        var added = await _unitOfWork.StudyAreas.AddItem(studyArea);
        await _unitOfWork.SaveChanges();

        return _mapper.Map<CreateStudyAreaDto>(added);
    }

    public async Task<int> UpdateStudyArea(UpdateStudyAreaDto updateStudyAreaDto)
    {
        var studyArea = _mapper.Map<StudyArea>(updateStudyAreaDto);
        
        await _unitOfWork.StudyAreas.UpdateItem(studyArea);
        var changes = await _unitOfWork.SaveChanges();

        if (changes < 1)
        {
            throw new Exception("Unable to update object");
        }

        return changes;
    }

    public async Task<int> DeleteStudyArea(int id)
    {
        await _unitOfWork.StudyAreas.DeleteItem(id);
        var changes = await _unitOfWork.SaveChanges();

        if (changes < 1)
        {
            throw new Exception($"Unable to delete object with id = {id}");
        }

        return changes;
    }

    public async Task<IEnumerable<GetStudyAreaListDto>> GetStudyAreas()
    {
        var studyAreas = await _unitOfWork.StudyAreas.GetItems();
        
        return _mapper.Map<IEnumerable<GetStudyAreaListDto>>(studyAreas);
    }

    public async Task<GetStudyAreaDetailDto?> GetStudyArea(int id)
    {
        var studyArea = await _unitOfWork.StudyAreas.GetItem(id);

        return _mapper.Map<GetStudyAreaDetailDto>(studyArea);
    }
}