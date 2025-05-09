using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestMaster.Core.Common;
using TestMaster.Core.Common.Interfaces;
using TestMaster.Infrastructure.Data;

namespace TestMaster.Infrastructure.Repositories
{
    /// <summary>
    /// Base repository implementation for data access operations
    /// </summary>
    /// <typeparam name="T">Entity type that inherits from BaseEntity</typeparam>
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Database context
        /// </summary>
        protected readonly ApplicationDbContext _dbContext;

        /// <summary>
        /// DbSet for the entity
        /// </summary>
        protected readonly DbSet<T> _dbSet;

        /// <summary>
        /// Initializes a new instance of BaseRepository
        /// </summary>
        /// <param name="dbContext">Database context</param>
        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        /// <inheritdoc/>
        public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FindAsync(new object[] { id }, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.ToListAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> filterExpression, CancellationToken cancellationToken = default)
        {
            return await _dbSet.Where(filterExpression).ToListAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<(IReadOnlyList<T> Items, int TotalCount)> GetPagedAsync(
            Expression<Func<T, bool>> filterExpression,
            Expression<Func<T, object>> orderBy,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            var query = _dbSet.AsQueryable();
            
            // Apply filtering
            if (filterExpression != null)
            {
                query = query.Where(filterExpression);
            }

            // Get total count
            var totalCount = await query.CountAsync(cancellationToken);

            // Apply ordering and pagination
            if (orderBy != null)
            {
                query = query.OrderBy(orderBy);
            }

            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            // Execute query
            var items = await query.ToListAsync(cancellationToken);

            return (items, totalCount);
        }

        /// <inheritdoc/>
        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            return entity;
        }

        /// <inheritdoc/>
        public Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return Task.FromResult(entity);
        }

        /// <inheritdoc/>
        public Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task SoftDeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            entity.IsActive = false;
            _dbContext.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> filterExpression, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AnyAsync(filterExpression, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<int> CountAsync(Expression<Func<T, bool>> filterExpression, CancellationToken cancellationToken = default)
        {
            return await _dbSet.CountAsync(filterExpression, cancellationToken);
        }
    }
} 