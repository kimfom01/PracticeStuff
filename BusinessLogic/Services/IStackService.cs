using DataAccess.Models;

namespace BusinessLogic.Services;

public interface IStackService
{
    Task<Stack?> AddStack(Stack stack);
    Task<int> UpdateStack(Stack stack);
    Task<int> DeleteStack(int id);
    Task<IEnumerable<Stack>> GetStacks();
    Task<Stack?> GetStack(int id);
}