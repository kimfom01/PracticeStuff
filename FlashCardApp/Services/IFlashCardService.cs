using FlashCardApp.Models;

namespace FlashCardApp.Services;

public interface IFlashCardService
{
    void ViewFlashCardSettingsMenu();
    void ManageFlashCardSettings(Stack stack);
    void ViewFlashCards(Stack stack);
    void AddFlashCardToStack(Stack stack);
    void ViewEditFlashCardMenu();
    void EditFlashCard(Stack stack);
    void EditAll(Stack stack);
    void EditFlashCardName(Stack stack);
    void EditBack(Stack stack);
    void DeleteFlashCard(Stack stack);
    void ViewFlashCardOfStack(Stack stack);
    void CreateFlashCardTable();
}
