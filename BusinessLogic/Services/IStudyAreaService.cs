using BusinessLogic.Enums;
using DataAccess.Dtos;
using DataAccess.Models;

namespace BusinessLogic.Services;

public interface IStudyAreaService
{
    StudyAreaOptions GetStudyAreaChoice();
    Task ManageStudyArea();
    Task ViewHistory();
    void ViewNewLessonMenu();
    Task StartLesson();
    int PlayLessonLoop(IEnumerable<FlashCardDto> flashCards);
    Task SaveScore(StudyArea studyArea, Stack stack);
    Task CreateStudyAreaTable();
}
