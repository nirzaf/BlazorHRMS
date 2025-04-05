using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BlazorHRMS.Infrastructure.Repositories
{
    /// <summary>
    /// Generic repository interface for CRUD operations
    /// </summary>
    /// <typeparam name="T">The entity type</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Gets all entities
        /// </summary>
        /// <returns>A collection of all entities</returns>
        Task<IEnumerable<T>> GetAllAsync();
        
        /// <summary>
        /// Gets entities that match the specified filter
        /// </summary>
        /// <param name="filter">The filter expression</param>
        /// <returns>A collection of matching entities</returns>
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> filter);
        
        /// <summary>
        /// Gets a single entity by ID
        /// </summary>
        /// <param name="id">The entity ID</param>
        /// <returns>The entity if found, null otherwise</returns>
        Task<T> GetByIdAsync(string id);
        
        /// <summary>
        /// Adds a new entity
        /// </summary>
        /// <param name="entity">The entity to add</param>
        /// <returns>The added entity</returns>
        Task<T> AddAsync(T entity);
        
        /// <summary>
        /// Updates an existing entity
        /// </summary>
        /// <param name="id">The entity ID</param>
        /// <param name="entity">The updated entity</param>
        /// <returns>True if the update was successful, false otherwise</returns>
        Task<bool> UpdateAsync(string id, T entity);
        
        /// <summary>
        /// Removes an entity
        /// </summary>
        /// <param name="id">The entity ID</param>
        /// <returns>True if the removal was successful, false otherwise</returns>
        Task<bool> RemoveAsync(string id);
        
        /// <summary>
        /// Soft deletes an entity by setting IsDeleted flag
        /// </summary>
        /// <param name="id">The entity ID</param>
        /// <returns>True if the soft delete was successful, false otherwise</returns>
        Task<bool> SoftDeleteAsync(string id);
    }
}