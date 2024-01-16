using PracticeStuff.Application.Dtos.Stack;

namespace PracticeStuff.Application.Services;

public interface IStackService
{
    Task<CreateStackDto?> AddStack(CreateStackDto createStackDto);
    Task<int> UpdateStack(UpdateStackDto updateStackDto);
    Task<int> DeleteStack(int id);
    Task<IEnumerable<GetStackListDto>> GetStacks();
    Task<GetStackDetailDto?> GetStack(int id);
}