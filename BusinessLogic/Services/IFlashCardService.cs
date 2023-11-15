using DataAccess.Models;

namespace BusinessLogic.Services;

public interface IFlashCardService
{
    Task<FlashCard?> AddFlashCard(FlashCard flashCard);
    Task<int> UpdateFlashCard(FlashCard flashCard);
    Task<int> DeleteFlashCard(int id);
    Task<IEnumerable<FlashCard>> GetFlashCards();
    Task<FlashCard?> GetFlashCard(int id);
}