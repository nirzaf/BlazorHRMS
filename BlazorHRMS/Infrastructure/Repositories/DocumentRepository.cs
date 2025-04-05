using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorHRMS.Domain;
using BlazorHRMS.Services;
using MongoDB.Driver;

namespace BlazorHRMS.Infrastructure.Repositories
{
    /// <summary>
    /// Repository implementation for Document entity operations
    /// </summary>
    public class DocumentRepository : BaseRepository<Document>, IDocumentRepository
    {
        public DocumentRepository(IMongoDBService mongoDBService) 
            : base(mongoDBService, "documents")
        {
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Document>> GetDocumentsByEntityAsync(string entityType, string entityId)
        {
            var filter = Builders<Document>.Filter.And(
                Builders<Document>.Filter.Eq(d => d.EntityType, entityType),
                Builders<Document>.Filter.Eq(d => d.EntityId, entityId),
                Builders<Document>.Filter.Eq(d => d.IsDeleted, false)
            );
            
            return await Collection.Find(filter).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Document>> GetDocumentsByCategoryAsync(string category)
        {
            var filter = Builders<Document>.Filter.And(
                Builders<Document>.Filter.Eq(d => d.Category, category),
                Builders<Document>.Filter.Eq(d => d.IsDeleted, false)
            );
            
            return await Collection.Find(filter).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Document>> GetDocumentsByUploaderAsync(string uploadedById)
        {
            var filter = Builders<Document>.Filter.And(
                Builders<Document>.Filter.Eq(d => d.UploadedById, uploadedById),
                Builders<Document>.Filter.Eq(d => d.IsDeleted, false)
            );
            
            return await Collection.Find(filter).ToListAsync();
        }
    }
}