using DataAccess.Models;

namespace DataAccess.Repositories;

public interface IFlashCardRepository : IRepositoryBase<FlashCard>
{
    Task UpdateFlashCardFront(FlashCard flashCard);
    Task UpdateFlashCardBack(FlashCard flashCard);
}