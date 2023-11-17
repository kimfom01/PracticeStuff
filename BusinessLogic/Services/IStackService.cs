using DataAccess.Dtos.Stack;
using DataAccess.Models;

namespace BusinessLogic.Services;

public interface IStackService
{
    Task<CreateStackDto?> AddStack(CreateStackDto createStackDto);
    Task<int> UpdateStack(UpdateStackDto updateStackDto);
    Task<int> DeleteStack(int id);
    Task<IEnumerable<GetStackListDto>> GetStacks();
    Task<GetStackDetailDto?> GetStack(int id);
}