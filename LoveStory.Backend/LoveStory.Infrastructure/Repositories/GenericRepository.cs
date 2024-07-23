using System.Linq.Expressions;
using LoveStory.Infrastructure.Contexts;
using LoveStory.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LoveStory.Infrastructure.Repositories;

public class GenericRepository<T>(LoveStoryContext context) : IRepository<T> where T : class
{
    public virtual IQueryable<T> GetAll() => context.Set<T>();
    public IAsyncEnumerable<T> GetAllAsync() => context.Set<T>().AsAsyncEnumerable();

    public Task<T?> GetOneAsync(Expression<Func<T, bool>> prediction) => context.Set<T>().FirstOrDefaultAsync(prediction);

    public async Task<bool> InsertAsync(T entity)
    {
        await context.Set<T>().AddAsync(entity);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> InsertMultipleAsync(ICollection<T> entities)
    {
        await context.Set<T>().AddRangeAsync(entities);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        context.Attach(entity).State = EntityState.Modified;
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateMultipleAsync(ICollection<T> entities)
    {
        context.Attach(entities).State = EntityState.Modified;
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(T entity)
    {
        context.Set<T>().Remove(entity);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteMultipleAsync(ICollection<T> entities)
    {
        context.Set<T>().RemoveRange(entities);
        return await context.SaveChangesAsync() > 0;
    }
}