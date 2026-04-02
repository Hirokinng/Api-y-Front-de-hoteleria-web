using Microsoft.EntityFrameworkCore;
using HoteleriaApp.Core.Application.Services;
using HoteleriaApp.Infrastructure.Persistence.Contexts;
using HoteleriaApp.Core.Application.Interfaces;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HoteleriaApp.Infrastructure.Persistence.Repositories
{

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly IApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(IApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(Guid id) =>
            await _dbSet.FindAsync(id);

        public async Task<IReadOnlyList<T>> GetAllAsync() =>
            await _dbSet.AsNoTracking().ToListAsync();

        public async Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate) =>
            await _dbSet.AsNoTracking().Where(predicate).ToListAsync();

        public async Task AddAsync(T entity) =>
            await _dbSet.AddAsync(entity);

        public void Update(T entity) =>
            _dbSet.Update(entity);

        public void Remove(T entity) =>
            _dbSet.Remove(entity);

        public Task<int> SaveChangesAsync() =>

            _context.SaveChangesAsync();

    }

}