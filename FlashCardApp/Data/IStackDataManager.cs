using FlashCardApp.DTO;
using FlashCardApp.Models;

namespace FlashCardApp.Data;

public interface IStackDataManager
{
    public void CreateStackTable();
    public void AddNewStack(Stack newStack);
    public void UpdateStack(Stack oldStack, Stack newStack);
    public void DeleteStack(Stack stackToDelete);
    public List<StackDTO> GetStacks();
    public int GetStackId(Stack stack);
}
