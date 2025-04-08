using Microsoft.EntityFrameworkCore;
using RPA.Core.Entities;
using RPA.Core.Repositories;
using RPA.Infrastructure.Data;

namespace RPA.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly RPADbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(RPADbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<T?>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<T?> AddAsync(T? entity)
        {
            entity!.CreatedAt = DateTime.UtcNow;
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task UpdateAsync(T? entity)
        {
            entity!.UpdatedAt = DateTime.UtcNow;
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            entity!.IsActive = false;
            await UpdateAsync(entity);
        }
    }
} 