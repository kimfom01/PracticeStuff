using DataAccess.Dtos;
using DataAccess.Models;

namespace DataAccess.Repositories;

public interface IStackRepository
{
    public Task CreateStackTable();
    public Task<int> AddNewStack(Stack newStack);
    public Task<int> UpdateStack(Stack oldStack, Stack newStack);
    public Task<int> DeleteStack(Stack stackToDelete);
    public Task<IEnumerable<StackDto>> GetStacks();
    public Task<int> GetStackId(Stack stack);
}
