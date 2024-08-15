using System.Linq.Expressions;
using EventWeb.Core.Abstractions.Repositories;
using EventWeb.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;

namespace EventWeb.DataAccess.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly EventContext _context; 
        

        public BaseRepository(EventContext context)
        {
            _context = context; 
        }

        public async Task<IEnumerable<T>> GetRangeAsync(Expression<Func<T, bool>> condition)
        {
            return await _context.Set<T>().Where(condition)
            .AsNoTracking()
            .ToListAsync();
        }

        public async Task<T?> GetSingleAsync(Expression<Func<T, bool>> condition)
        {
            return await _context.Set<T>().Where(condition)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);     
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity); 
        }     
    }
}