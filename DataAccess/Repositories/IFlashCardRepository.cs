using DataAccess.Dtos;
using DataAccess.Models;

namespace DataAccess.Repositories;

public interface IFlashCardRepository
{
    public Task CreateFlashCardTable();
    public Task<int> AddNewFlashCard(FlashCard flashCard, Stack stack);
    public Task<int> UpdateFlashCard(FlashCard oldFlashCard, FlashCard newFlashCard, Stack stack);
    public Task<int> UpdateFlashCardName(FlashCard oldFlashCard, FlashCard newFlashCard, Stack stack);
    public Task<int> UpdateFlashCardContent(FlashCard oldFlashCard, FlashCard newFlashCard, Stack stack);
    public Task<int> DeleteFlashCard(FlashCard flashCardToDelete, Stack stack);
    public Task<IEnumerable<FlashCardDto>> GetFlashCardsOfStack(Stack stack);
}