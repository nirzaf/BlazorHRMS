using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorHRMS.Domain;

namespace BlazorHRMS.Infrastructure.Repositories
{
    /// <summary>
    /// Repository interface for Document entity operations
    /// </summary>
    public interface IDocumentRepository : IRepository<Document>
    {
        /// <summary>
        /// Gets documents by entity type and ID
        /// </summary>
        /// <param name="entityType">The entity type (e.g., Employee, LeaveRequest, PerformanceReview)</param>
        /// <param name="entityId">The entity ID</param>
        /// <returns>A collection of documents associated with the specified entity</returns>
        Task<IEnumerable<Document>> GetDocumentsByEntityAsync(string entityType, string entityId);
        
        /// <summary>
        /// Gets documents by category
        /// </summary>
        /// <param name="category">The document category</param>
        /// <returns>A collection of documents in the specified category</returns>
        Task<IEnumerable<Document>> GetDocumentsByCategoryAsync(string category);
        
        /// <summary>
        /// Gets documents uploaded by a specific user
        /// </summary>
        /// <param name="uploadedById">The ID of the user who uploaded the documents</param>
        /// <returns>A collection of documents uploaded by the specified user</returns>
        Task<IEnumerable<Document>> GetDocumentsByUploaderAsync(string uploadedById);
    }
}