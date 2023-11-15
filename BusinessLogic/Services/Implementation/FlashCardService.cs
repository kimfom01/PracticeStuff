using DataAccess.Models;
using DataAccess.Repositories;

namespace BusinessLogic.Services.Implementation;

public class FlashCardService : IFlashCardService
{
    private readonly IUnitOfWork _unitOfWork;

    public FlashCardService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<FlashCard?> AddFlashCard(FlashCard flashCard)
    {
        var added = await _unitOfWork.FlashCards.AddItem(flashCard);
        await _unitOfWork.SaveChanges();

        return added;
    }

    public async Task<int> UpdateFlashCard(FlashCard flashCard)
    {
        await _unitOfWork.FlashCards.UpdateItem(flashCard);
        var changes = await _unitOfWork.SaveChanges();

        if (changes < 1)
        {
            throw new Exception("Unable to update object");
        }

        return changes;
    }

    public async Task<int> DeleteFlashCard(int id)
    {
        await _unitOfWork.FlashCards.DeleteItem(id);
        var changes = await _unitOfWork.SaveChanges();

        if (changes < 1)
        {
            throw new Exception($"Unable to delete object with id = {id}");
        }

        return changes;
    }

    public async Task<IEnumerable<FlashCard>> GetFlashCards()
    {
        return await _unitOfWork.FlashCards.GetItems();
    }

    public async Task<FlashCard?> GetFlashCard(int id)
    {
        return await _unitOfWork.FlashCards.GetItem(id);
    }
}