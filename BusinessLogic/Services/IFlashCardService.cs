using BusinessLogic.Enums;
using DataAccess.Models;

namespace BusinessLogic.Services;

public interface IFlashCardService
{
    FlashCardSettingsOptions GetFlashCardSettingsChoice();
    Task ManageFlashCardSettings(Stack stack);
    Task ViewFlashCards(Stack stack);
    Task AddFlashCardToStack(Stack stack);
    EditFlashCardOptions GetEditFlashCardChoice();
    Task EditFlashCard(Stack stack);
    Task EditAll(Stack stack);
    Task EditFlashCardName(Stack stack);
    Task EditBack(Stack stack);
    Task DeleteFlashCard(Stack stack);
    Task ViewFlashCardOfStack(Stack stack);
    Task CreateFlashCardTable();
}
