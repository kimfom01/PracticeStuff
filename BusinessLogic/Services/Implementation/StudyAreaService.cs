using DataAccess.Models;
using DataAccess.Repositories;

namespace BusinessLogic.Services.Implementation;

public class StudyAreaService : IStudyAreaService
{
    private readonly IUnitOfWork _unitOfWork;

    public StudyAreaService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<StudyArea?> AddStudyArea(StudyArea studyArea)
    {
        var added = await _unitOfWork.StudyAreas.AddItem(studyArea);
        await _unitOfWork.SaveChanges();

        return added;
    }

    public async Task<int> UpdateStudyArea(StudyArea studyArea)
    {
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

    public async Task<IEnumerable<StudyArea>> GetStudyAreas()
    {
        return await _unitOfWork.StudyAreas.GetItems();
    }

    public async Task<StudyArea?> GetStudyArea(int id)
    {
        return await _unitOfWork.StudyAreas.GetItem(id);
    }
}