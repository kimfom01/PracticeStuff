using DataAccess.Models;
using DataAccess.Repositories;

namespace BusinessLogic.Services.Implementation;

public class StackService : IStackService
{
    private readonly IUnitOfWork _unitOfWork;

    public StackService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Stack?> AddStack(Stack stack)
    {
        var added = await _unitOfWork.Stacks.AddItem(stack);
        await _unitOfWork.SaveChanges();

        return added;
    }

    public async Task<int> UpdateStack(Stack stack)
    {
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

    public async Task<IEnumerable<Stack>> GetStacks()
    {
        return await _unitOfWork.Stacks.GetItems();
    }

    public async Task<Stack?> GetStack(int id)
    {
        return await _unitOfWork.Stacks.GetItem(id);
    }
}