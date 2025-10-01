using LibraryManagementSystem.Application.Contracts.Repositories;
using LibraryManagementSystem.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibraryManagementSystem.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private ApplicationDbContext _context;
        private DbSet<T> _dbSet;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

        public async Task AddRangeAsync(IEnumerable<T> entities) => await _dbSet.AddRangeAsync(entities);

        public Task<bool> AnyAsync() => _dbSet.AsNoTracking().AnyAsync();

        public Task<bool> AnyAsync(Expression<Func<T, bool>> filter) => _dbSet.AsNoTracking().AnyAsync(filter);

        public Task<int> CountAsync() => _dbSet.AsNoTracking().CountAsync();

        public Task<int> CountAsync(Expression<Func<T, bool>> filter) => _dbSet.AsNoTracking().CountAsync(filter);
        public async Task<T?> FindAsync(params object[] keyValues) => await _dbSet.FindAsync(keyValues);

        public IQueryable<T> GetAll(Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            if (filter != null)
                query = query.Where(filter);

            return query;
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            if (filter != null)
                query = query.Where(filter);

            return await query.FirstOrDefaultAsync();
        }

        public void Remove(T entity) => _dbSet.Remove(entity);

        public void RemoveRange(IEnumerable<T> entities) => _dbSet.RemoveRange(entities);

        public void Update(T entity) => _dbSet.Update(entity);

        public IQueryable<T> GetWithDetails() => _dbSet.AsQueryable();
    }
}
