using FlashCardApp.DTO;
using FlashCardApp.Models;

namespace FlashCardApp.Data;

public interface IStudyAreaDataManager
{
    public void CreateStudyAreaTable();
    public void SaveScore(StudyArea studyArea, Stack stack);
    public List<StudyAreaDto> GetScoresHistory();
}
