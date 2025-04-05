using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorHRMS.Domain;
using BlazorHRMS.Services;
using MongoDB.Driver;

namespace BlazorHRMS.Infrastructure.Repositories
{
    /// <summary>
    /// Repository implementation for PerformanceReview entity operations
    /// </summary>
    public class PerformanceReviewRepository : BaseRepository<PerformanceReview>, IPerformanceReviewRepository
    {
        public PerformanceReviewRepository(IMongoDBService mongoDBService) 
            : base(mongoDBService, "performanceReviews")
        {
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<PerformanceReview>> GetReviewsByEmployeeAsync(string employeeId)
        {
            var filter = Builders<PerformanceReview>.Filter.And(
                Builders<PerformanceReview>.Filter.Eq(r => r.EmployeeId, employeeId),
                Builders<PerformanceReview>.Filter.Eq(r => r.IsDeleted, false)
            );
            
            return await Collection.Find(filter).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<PerformanceReview>> GetReviewsByReviewerAsync(string reviewerId)
        {
            var filter = Builders<PerformanceReview>.Filter.And(
                Builders<PerformanceReview>.Filter.Eq(r => r.ReviewerId, reviewerId),
                Builders<PerformanceReview>.Filter.Eq(r => r.IsDeleted, false)
            );
            
            return await Collection.Find(filter).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<PerformanceReview>> GetReviewsByStatusAsync(string status)
        {
            var filter = Builders<PerformanceReview>.Filter.And(
                Builders<PerformanceReview>.Filter.Eq(r => r.Status, status),
                Builders<PerformanceReview>.Filter.Eq(r => r.IsDeleted, false)
            );
            
            return await Collection.Find(filter).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<PerformanceReview>> GetReviewsByPeriodAsync(string reviewPeriod)
        {
            var filter = Builders<PerformanceReview>.Filter.And(
                Builders<PerformanceReview>.Filter.Eq(r => r.ReviewPeriod, reviewPeriod),
                Builders<PerformanceReview>.Filter.Eq(r => r.IsDeleted, false)
            );
            
            return await Collection.Find(filter).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateReviewStatusAsync(string id, string status)
        {
            var filter = Builders<PerformanceReview>.Filter.Eq(r => r.Id, id);
            
            var update = Builders<PerformanceReview>.Update
                .Set(r => r.Status, status)
                .Set(r => r.UpdatedAt, System.DateTime.UtcNow);
                
            var result = await Collection.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }
    }
    
    /// <summary>
    /// Repository implementation for ReviewTemplate entity operations
    /// </summary>
    public class ReviewTemplateRepository : BaseRepository<ReviewTemplate>, IReviewTemplateRepository
    {
        public ReviewTemplateRepository(IMongoDBService mongoDBService) 
            : base(mongoDBService, "reviewTemplates")
        {
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<ReviewTemplate>> GetActiveTemplatesAsync()
        {
            var filter = Builders<ReviewTemplate>.Filter.Eq(t => t.IsActive, true);
            return await Collection.Find(filter).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<ReviewTemplate> GetTemplateByNameAsync(string name)
        {
            var filter = Builders<ReviewTemplate>.Filter.Eq(t => t.Name, name);
            return await Collection.Find(filter).FirstOrDefaultAsync();
        }
    }
}