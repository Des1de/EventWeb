using System.Linq.Expressions;

namespace EventWeb.Core.Abstractions.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<T?> GetSingleAsync(Expression<Func<T, bool>> condition); 
        Task<IEnumerable<T>> GetRangeAsync(Expression<Func<T, bool>> condition); 
        void Create(T entity);
        void Update(T entity); 
        void Delete(T entity);  

    }
}