using System.Linq.Expressions;

namespace PracticeStuff.Persistence.Repositories;

public interface IRepositoryBase<TEntity>
{
    Task<TEntity?> AddItem(TEntity entity);
    Task<TEntity?> GetItem(int id);
    Task<IEnumerable<TEntity>> GetItems();
    Task<IEnumerable<TEntity>> GetItems(Expression<Func<TEntity, bool>> expression);
    Task UpdateItem(TEntity entity);
    Task DeleteItem(int id);
    Task DeleteItem(Expression<Func<TEntity, bool>> expression);
}