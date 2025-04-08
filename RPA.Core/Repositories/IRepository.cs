using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RPA.Core.Entities;

namespace RPA.Core.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T?>> GetAllAsync();
        Task<T?> AddAsync(T? entity);
        Task UpdateAsync(T? entity);
        Task DeleteAsync(Guid id);
    }
} 