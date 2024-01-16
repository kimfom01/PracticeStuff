using AutoMapper;
using PracticeStuff.Core;
using PracticeStuff.Application.Dtos.Stack;
using PracticeStuff.Persistence.Repositories;

namespace PracticeStuff.Application.Services.Implementation;

public class StackService : IStackService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public StackService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CreateStackDto?> AddStack(CreateStackDto createStackDto)
    {
        var stack = _mapper.Map<Stack>(createStackDto);
        
        var added = await _unitOfWork.Stacks.AddItem(stack);
        await _unitOfWork.SaveChanges();

        return _mapper.Map<CreateStackDto>(added);
    }

    public async Task<int> UpdateStack(UpdateStackDto updateStackDto)
    {
        var stack = _mapper.Map<Stack>(updateStackDto);
        
        await _unitOfWork.Stacks.UpdateItem(stack);
        var changes = await _unitOfWork.SaveChanges();

        if (changes < 1)
        {
            throw new Exception("Unable to update object");
        }

        return changes;
    }

    public async Task<int> DeleteStack(int id)
    {
        await _unitOfWork.Stacks.DeleteItem(id);
        var changes = await _unitOfWork.SaveChanges();

        if (changes < 1)
        {
            throw new Exception($"Unable to delete object with id = {id}");
        }

        return changes;
    }

    public async Task<IEnumerable<GetStackListDto>> GetStacks()
    {
        var stacks = await _unitOfWork.Stacks.GetItems();
        
        return _mapper.Map<IEnumerable<GetStackListDto>>(stacks);
    }

    public async Task<GetStackDetailDto?> GetStack(int id)
    {
        var stack = await _unitOfWork.Stacks.GetItem(id);
        
        return _mapper.Map<GetStackDetailDto>(stack);
    }
}