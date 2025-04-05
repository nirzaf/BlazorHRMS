using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorHRMS.Domain;

namespace BlazorHRMS.Infrastructure.Repositories
{
    /// <summary>
    /// Repository interface for PerformanceReview entity operations
    /// </summary>
    public interface IPerformanceReviewRepository : IRepository<PerformanceReview>
    {
        /// <summary>
        /// Gets performance reviews by employee ID
        /// </summary>
        /// <param name="employeeId">The employee ID</param>
        /// <returns>A collection of performance reviews for the specified employee</returns>
        Task<IEnumerable<PerformanceReview>> GetReviewsByEmployeeAsync(string employeeId);
        
        /// <summary>
        /// Gets performance reviews by reviewer ID
        /// </summary>
        /// <param name="reviewerId">The reviewer ID</param>
        /// <returns>A collection of performance reviews assigned to the specified reviewer</returns>
        Task<IEnumerable<PerformanceReview>> GetReviewsByReviewerAsync(string reviewerId);
        
        /// <summary>
        /// Gets performance reviews by status
        /// </summary>
        /// <param name="status">The review status</param>
        /// <returns>A collection of performance reviews with the specified status</returns>
        Task<IEnumerable<PerformanceReview>> GetReviewsByStatusAsync(string status);
        
        /// <summary>
        /// Gets performance reviews by review period
        /// </summary>
        /// <param name="reviewPeriod">The review period (e.g., "2025 Q1")</param>
        /// <returns>A collection of performance reviews for the specified period</returns>
        Task<IEnumerable<PerformanceReview>> GetReviewsByPeriodAsync(string reviewPeriod);
        
        /// <summary>
        /// Updates the status of a performance review
        /// </summary>
        /// <param name="id">The review ID</param>
        /// <param name="status">The new status</param>
        /// <returns>True if the update was successful, false otherwise</returns>
        Task<bool> UpdateReviewStatusAsync(string id, string status);
    }
    
    /// <summary>
    /// Repository interface for ReviewTemplate entity operations
    /// </summary>
    public interface IReviewTemplateRepository : IRepository<ReviewTemplate>
    {
        /// <summary>
        /// Gets active review templates
        /// </summary>
        /// <returns>A collection of active review templates</returns>
        Task<IEnumerable<ReviewTemplate>> GetActiveTemplatesAsync();
        
        /// <summary>
        /// Gets a review template by name
        /// </summary>
        /// <param name="name">The template name</param>
        /// <returns>The review template if found, null otherwise</returns>
        Task<ReviewTemplate> GetTemplateByNameAsync(string name);
    }
}