using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace BlazorHRMS.Services
{
    public class MongoDBService
    {
        private readonly IMongoDatabase _database;

        public MongoDBService(IConfiguration configuration)
        {
            // Retrieve the connection string from the secrets configuration
            var connectionString = configuration["MongoDB:ConnectionString"];
            
            // Create a MongoClient with the connection string
            var client = new MongoClient(connectionString);
            
            // Get the database (you can replace "hrms_db" with your actual database name)
            _database = client.GetDatabase("hrms_db");
        }

        // Example method to get a collection
        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }

        // Add more MongoDB-specific methods as needed
    }
}