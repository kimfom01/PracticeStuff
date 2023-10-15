using DataAccess.DTO;
using DataAccess.Models;

namespace DataAccess.Data;

public interface IFlashCardDataManager
{
    public void CreateFlashCardTable();
    public void AddNewFlashCard(FlashCard flashCard, Stack stack);
    public void UpdateFlashCard(FlashCard oldFlashCard, FlashCard newFlashCard, Stack stack);
    public void UpdateFlashCardName(FlashCard oldFlashCard, FlashCard newFlashCard, Stack stack);
    public void UpdateFlashCardContent(FlashCard oldFlashCard, FlashCard newFlashCard, Stack stack);
    public void DeleteFlashCard(FlashCard flashCardToDelete, Stack stack);
    public List<FlashCardDTO> GetFlashCardsOfStack(Stack stack);
}