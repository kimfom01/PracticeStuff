using System.Linq.Expressions;
using DataAccess.DataContext;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementation;

public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
{
    protected DbSet<TEntity> DbEntitySet { get; }

    protected RepositoryBase(Context context)
    {
        DbEntitySet = context.Set<TEntity>();
    }

    public async Task<TEntity?> AddItem(TEntity entity)
    {
        var added = await DbEntitySet.AddAsync(entity);

        return added.Entity;
    }

    public async Task<TEntity?> GetItem(int id)
    {
        return await DbEntitySet.FindAsync(id);
    }

    public async Task<TEntity?> GetItem(Expression<Func<TEntity, bool>> expression)
    {
        return await DbEntitySet.FirstOrDefaultAsync(expression);
    }

    public async Task<IEnumerable<TEntity>> GetItems()
    {
        return await Task.FromResult(DbEntitySet.AsNoTracking());
    }

    public async Task<IEnumerable<TEntity>> GetItems(Expression<Func<TEntity, bool>> expression)
    {
        return await Task.FromResult(DbEntitySet.Where(expression));
    }

    public async Task UpdateItem(TEntity entity)
    {
        await Task.FromResult(DbEntitySet.Update(entity));
    }

    public async Task DeleteItem(int id)
    {
        var item = await GetItem(id);

        if (item is not null)
        {
            await Task.FromResult(DbEntitySet.Remove(item));
        }
    }

    public async Task DeleteItem(Expression<Func<TEntity, bool>> expression)
    {
        var item = await GetItem(expression);

        if (item is not null)
        {
            await Task.FromResult(DbEntitySet.Remove(item));
        }
    }
}