using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace TestMaster.Core.Common.Interfaces
{
    /// <summary>
    /// Generic repository interface for data access operations
    /// </summary>
    /// <typeparam name="T">Entity type that inherits from BaseEntity</typeparam>
    public interface IRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Gets an entity by its ID
        /// </summary>
        /// <param name="id">Entity ID</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Entity if found, null if not found</returns>
        Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Gets all entities
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of all entities</returns>
        Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets entities that match the filter expression
        /// </summary>
        /// <param name="filterExpression">Filter expression</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Filtered list of entities</returns>
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> filterExpression, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets paged results with optional filtering and sorting
        /// </summary>
        /// <param name="filterExpression">Filter expression</param>
        /// <param name="orderBy">Order by expression</param>
        /// <param name="pageNumber">Page number (1-based)</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Paged list of entities</returns>
        Task<(IReadOnlyList<T> Items, int TotalCount)> GetPagedAsync(
            Expression<Func<T, bool>> filterExpression,
            Expression<Func<T, object>> orderBy,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds a new entity
        /// </summary>
        /// <param name="entity">Entity to add</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Added entity</returns>
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing entity
        /// </summary>
        /// <param name="entity">Entity to update</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Updated entity</returns>
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="entity">Entity to delete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Task representing the asynchronous operation</returns>
        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Soft deletes an entity by setting IsActive to false
        /// </summary>
        /// <param name="entity">Entity to soft delete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Task representing the asynchronous operation</returns>
        Task SoftDeleteAsync(T entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if any entity exists that matches the filter expression
        /// </summary>
        /// <param name="filterExpression">Filter expression</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>True if any entity matches, false otherwise</returns>
        Task<bool> ExistsAsync(Expression<Func<T, bool>> filterExpression, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the count of entities that match the filter expression
        /// </summary>
        /// <param name="filterExpression">Filter expression</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Count of matching entities</returns>
        Task<int> CountAsync(Expression<Func<T, bool>> filterExpression, CancellationToken cancellationToken = default);
    }
} 