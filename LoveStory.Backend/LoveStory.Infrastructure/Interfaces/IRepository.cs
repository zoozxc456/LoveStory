using System.Linq.Expressions;

namespace LoveStory.Infrastructure.Interfaces;

public interface IRepository<T>
{
    IQueryable<T> GetAll();
    IAsyncEnumerable<T> GetAllAsync();
    Task<T?> GetOneAsync(Expression<Func<T, bool>> prediction);
    Task<bool> InsertAsync(T entity);
    Task<bool> InsertMultipleAsync(IEnumerable<T> entities);
    Task<bool> UpdateAsync(T entity);
    Task<bool> UpdateMultipleAsync(List<T> entities);
    Task<bool> DeleteAsync(T entity);
    Task<bool> DeleteMultipleAsync(ICollection<T> entities);
}