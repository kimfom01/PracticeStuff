using PracticeStuff.Application.Dtos.FlashCard;

namespace PracticeStuff.Application.Services;

public interface IFlashCardService
{
    Task<CreateFlashCardDto?> AddFlashCard(CreateFlashCardDto createFlashCardDto);
    Task<int> UpdateFlashCard(UpdateFlashCardDto updateFlashCardDto);
    Task<int> UpdateFlashCardFront(UpdateFlashCardFrontDto updateFlashCardFrontDto);
    Task<int> UpdateFlashCardBack(UpdateFlashCardBackDto updateFlashCardBackDto);
    Task<int> DeleteFlashCard(int id);
    Task<IEnumerable<GetFlashCardListDto>> GetFlashCards();
    
    Task<IEnumerable<GetFlashCardListDto>> GetFlashCards(int stackId);
    Task<GetFlashCardDetailDto?> GetFlashCard(int stackId, int id);
}