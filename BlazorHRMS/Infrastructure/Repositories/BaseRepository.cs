using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BlazorHRMS.Services;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BlazorHRMS.Infrastructure.Repositories
{
    /// <summary>
    /// Base repository implementation for MongoDB
    /// </summary>
    /// <typeparam name="T">The entity type</typeparam>
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly IMongoCollection<T> Collection;
        protected readonly IMongoDBService MongoDBService;
        
        protected BaseRepository(IMongoDBService mongoDBService, string collectionName)
        {
            MongoDBService = mongoDBService;
            Collection = mongoDBService.GetCollection<T>(collectionName);
        }
        
        /// <inheritdoc/>
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Collection.Find(Builders<T>.Filter.Empty).ToListAsync();
        }
        
        /// <inheritdoc/>
        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> filter)
        {
            return await Collection.Find(filter).ToListAsync();
        }
        
        /// <inheritdoc/>
        public virtual async Task<T> GetByIdAsync(string id)
        {
            var objectId = new ObjectId(id);
            var filter = Builders<T>.Filter.Eq("_id", objectId);
            return await Collection.Find(filter).FirstOrDefaultAsync();
        }
        
        /// <inheritdoc/>
        public virtual async Task<T> AddAsync(T entity)
        {
            await Collection.InsertOneAsync(entity);
            return entity;
        }
        
        /// <inheritdoc/>
        public virtual async Task<bool> UpdateAsync(string id, T entity)
        {
            var objectId = new ObjectId(id);
            var filter = Builders<T>.Filter.Eq("_id", objectId);
            var result = await Collection.ReplaceOneAsync(filter, entity);
            return result.ModifiedCount > 0;
        }
        
        /// <inheritdoc/>
        public virtual async Task<bool> RemoveAsync(string id)
        {
            var objectId = new ObjectId(id);
            var filter = Builders<T>.Filter.Eq("_id", objectId);
            var result = await Collection.DeleteOneAsync(filter);
            return result.DeletedCount > 0;
        }
        
        /// <inheritdoc/>
        public virtual async Task<bool> SoftDeleteAsync(string id)
        {
            var objectId = new ObjectId(id);
            var filter = Builders<T>.Filter.Eq("_id", objectId);
            var update = Builders<T>.Update.Set("IsDeleted", true);
            var result = await Collection.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }
    }
}