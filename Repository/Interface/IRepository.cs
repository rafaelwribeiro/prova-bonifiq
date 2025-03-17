using System.Linq.Expressions;

namespace ProvaPub.Repository.Interface;

public interface IRepository<T> where T : class
{
    Task<IList<T>> GetAll();
    Task<T?> GetById(int id);
    Task<T?> GetByFilter(Expression<Func<T, bool>> filter);
    Task<IList<T>> GetManyByFilter(Expression<Func<T, bool>> filter);
    Task<IList<T>> GetManyByFilterPaginated(Expression<Func<T, bool>> filter, int pageNumber, int pageSize);
    Task<IList<T>> GetManyPaginated(int pageNumber, int pageSize);
    Task<int> CountAsync(Expression<Func<T, bool>> filter);
    Task<bool> AnyByFilter(Expression<Func<T, bool>> filter);
    Task<T> Add(T entity);
    void Update(T entity);
    Task<T> Delete(int id);
}
