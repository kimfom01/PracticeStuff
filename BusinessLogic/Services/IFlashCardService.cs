using BusinessLogic.Enums;
using DataAccess.Models;

namespace BusinessLogic.Services;

public interface IFlashCardService
{
    FlashCardSettingsOptions GetFlashCardSettingsChoice();
    void ManageFlashCardSettings(Stack stack);
    void ViewFlashCards(Stack stack);
    void AddFlashCardToStack(Stack stack);
    EditFlashCardOptions GetEditFlashCardChoice();
    void EditFlashCard(Stack stack);
    void EditAll(Stack stack);
    void EditFlashCardName(Stack stack);
    void EditBack(Stack stack);
    void DeleteFlashCard(Stack stack);
    void ViewFlashCardOfStack(Stack stack);
    void CreateFlashCardTable();
}
