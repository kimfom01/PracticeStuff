using DataAccess.DTO;
using DataAccess.Models;

namespace BusinessLogic.Services;

public interface IStudyAreaService
{
    void ViewStudyAreaMenu();
    void ManageStudyArea();
    void ViewHistory();
    void ViewNewLessonMenu();
    void StartLesson();
    int PlayLessonLoop(List<FlashCardDTO> flashCards);
    void SaveScore(StudyArea studyArea, Stack stack);
    void CreateStudyAreaTable();
}
