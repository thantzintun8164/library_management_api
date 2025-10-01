using System.Linq.Expressions;

namespace LibraryManagementSystem.Application.Contracts.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void Update(T entity);
        Task<T?> FindAsync(params object[] keyValues);
        Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[] includes);
        IQueryable<T> GetAll(Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[] includes);
        Task<bool> AnyAsync();
        Task<int> CountAsync();
        Task<bool> AnyAsync(Expression<Func<T, bool>> filter);
        Task<int> CountAsync(Expression<Func<T, bool>> filter);
        IQueryable<T> GetWithDetails();
    }
}
