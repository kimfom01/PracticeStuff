using DataAccess.Dtos.FlashCard;
using DataAccess.Models;

namespace BusinessLogic.Services;

public interface IFlashCardService
{
    Task<FlashCard?> AddFlashCard(CreateFlashCardDto createFlashCardDto);
    Task<int> UpdateFlashCard(UpdateFlashCardDto updateFlashCardDto);
    Task<int> UpdateFlashCardFront(UpdateFlashCardFrontDto updateFlashCardFrontDto);
    Task<int> UpdateFlashCardBack(UpdateFlashCardBackDto updateFlashCardBackDto);
    Task<int> DeleteFlashCard(int id);
    Task<IEnumerable<GetFlashCardDto>> GetFlashCards();
    Task<GetFlashCardDto?> GetFlashCard(int id);
}