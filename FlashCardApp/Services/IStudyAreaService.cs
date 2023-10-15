using FlashCardApp.DTO;
using FlashCardApp.Models;

namespace FlashCardApp.Services;

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
