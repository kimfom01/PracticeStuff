using AutoMapper;
using DataAccess.Dtos.FlashCard;
using DataAccess.Models;
using DataAccess.Repositories;

namespace BusinessLogic.Services.Implementation;

public class FlashCardService : IFlashCardService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public FlashCardService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<FlashCard?> AddFlashCard(CreateFlashCardDto createFlashCardDto)
    {
        var flashCard = _mapper.Map<FlashCard>(createFlashCardDto);
        
        var added = await _unitOfWork.FlashCards.AddItem(flashCard);
        await _unitOfWork.SaveChanges();

        return added;
    }

    public async Task<int> UpdateFlashCard(UpdateFlashCardDto updateFlashCardDto)
    {
        var flashCard = _mapper.Map<FlashCard>(updateFlashCardDto);
        
        await _unitOfWork.FlashCards.UpdateItem(flashCard);
        var changes = await _unitOfWork.SaveChanges();

        if (changes < 1)
        {
            throw new Exception("Unable to update object");
        }

        return changes;
    }

    public async Task<int> UpdateFlashCardFront(UpdateFlashCardFrontDto updateFlashCardFrontDto)
    {
        var flashCard = _mapper.Map<FlashCard>(updateFlashCardFrontDto);

        await _unitOfWork.FlashCards.UpdateFlashCardFront(flashCard);
        return await _unitOfWork.SaveChanges();
    }

    public async Task<int> UpdateFlashCardBack(UpdateFlashCardBackDto updateFlashCardBackDto)
    {
        var flashCard = _mapper.Map<FlashCard>(updateFlashCardBackDto);

        await _unitOfWork.FlashCards.UpdateFlashCardBack(flashCard);
        return await _unitOfWork.SaveChanges();
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

    public async Task<IEnumerable<GetFlashCardDto>> GetFlashCards()
    {
        var flashCards = await _unitOfWork.FlashCards.GetItems();

        return _mapper.Map<IEnumerable<GetFlashCardDto>>(flashCards);
    }

    public async Task<GetFlashCardDto?> GetFlashCard(int id)
    {
        var flashCard = await _unitOfWork.FlashCards.GetItem(id);

        return _mapper.Map<GetFlashCardDto>(flashCard);
    }
}