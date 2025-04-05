using MongoDB.Driver;

namespace BlazorHRMS.Services
{
    /// <summary>
    /// Interface for MongoDB database operations
    /// Follows the dependency injection pattern for better testability
    /// </summary>
    public interface IMongoDBService
    {
        /// <summary>
        /// Gets a MongoDB collection of the specified type
        /// </summary>
        /// <typeparam name="T">The type of documents in the collection</typeparam>
        /// <param name="collectionName">The name of the collection</param>
        /// <returns>A MongoDB collection</returns>
        IMongoCollection<T> GetCollection<T>(string collectionName);
        
        /// <summary>
        /// Gets the MongoDB database instance
        /// </summary>
        /// <returns>The MongoDB database</returns>
        IMongoDatabase GetDatabase();
    }
}