using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using System;

namespace BlazorHRMS.Services
{
    /// <summary>
    /// Service for MongoDB database operations
    /// Implements the IMongoDBService interface for dependency injection
    /// </summary>
    public class MongoDBService : IMongoDBService
    {
        private readonly IMongoDatabase _database;
        private readonly ILogger<MongoDBService> _logger;
        private readonly MongoClient _client;

        public MongoDBService(IConfiguration configuration, ILogger<MongoDBService> logger)
        {
            _logger = logger;
            
            try
            {
                // Retrieve the connection string from the secrets configuration
                var connectionString = configuration["MongoDB:ConnectionString"];
                
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException("MongoDB connection string is not configured. Please check appsettings.secrets.json file.");
                }
                
                // Configure MongoDB client settings
                var settings = MongoClientSettings.FromConnectionString(connectionString);
                
                // Set the ServerApi field of the settings object to set the version of the Stable API
                settings.ServerApi = new ServerApi(ServerApiVersion.V1);
                
                // Create a MongoClient with the connection string
                _client = new MongoClient(settings);
                
                // Get the database
                _database = _client.GetDatabase("hrms_db");
                
                // Ping the database to verify connection
                var result = _database.RunCommand<BsonDocument>(new BsonDocument("ping", 1));
                _logger.LogInformation("Successfully connected to MongoDB database.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to connect to MongoDB database.");
                throw; // Rethrow to prevent application from starting with invalid database connection
            }
        }

        /// <summary>
        /// Gets a MongoDB collection of the specified type
        /// </summary>
        /// <typeparam name="T">The type of documents in the collection</typeparam>
        /// <param name="collectionName">The name of the collection</param>
        /// <returns>A MongoDB collection</returns>
        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            try
            {
                return _database.GetCollection<T>(collectionName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting collection {collectionName}");
                throw;
            }
        }
        
        /// <summary>
        /// Gets the MongoDB database instance
        /// </summary>
        /// <returns>The MongoDB database</returns>
        public IMongoDatabase GetDatabase()
        {
            return _database;
        }
    }
}