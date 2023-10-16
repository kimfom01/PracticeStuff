using BusinessLogic.Enums;
using DataAccess.DTO;
using DataAccess.Models;

namespace BusinessLogic.Services;

public interface IStudyAreaService
{
    StudyAreaOptions GetStudyAreaChoice();
    void ManageStudyArea();
    void ViewHistory();
    void ViewNewLessonMenu();
    void StartLesson();
    int PlayLessonLoop(List<FlashCardDTO> flashCards);
    void SaveScore(StudyArea studyArea, Stack stack);
    void CreateStudyAreaTable();
}
