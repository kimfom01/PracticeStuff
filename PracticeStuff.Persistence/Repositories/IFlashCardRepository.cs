using PracticeStuff.Core;

namespace PracticeStuff.Persistence.Repositories;

public interface IFlashCardRepository : IRepositoryBase<FlashCard>
{
    Task UpdateFlashCardFront(FlashCard flashCard);
    Task UpdateFlashCardBack(FlashCard flashCard);
    Task<FlashCard?> GetFlashCard(int stackId, int id);
}