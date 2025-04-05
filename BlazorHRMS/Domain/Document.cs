using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BlazorHRMS.Domain
{
    /// <summary>
    /// Represents a document uploaded to the system.
    /// Stores metadata while actual files are stored in cloud storage.
    /// </summary>
    public class Document
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        
        public string FileName { get; set; } = null!;
        
        public string ContentType { get; set; } = null!;
        
        public long FileSize { get; set; }
        
        // Reference to the storage location (e.g., blob storage URL or path)
        public string StorageReference { get; set; } = null!;
        
        // Entity type this document is associated with (Employee, LeaveRequest, PerformanceReview)
        public string EntityType { get; set; } = null!;
        
        // ID of the entity this document is associated with
        [BsonRepresentation(BsonType.ObjectId)]
        public string EntityId { get; set; } = null!;
        
        // Document category (e.g., Resume, Contract, ID Proof, Medical Certificate)
        public string Category { get; set; } = null!;
        
        // Optional description
        public string? Description { get; set; }
        
        [BsonRepresentation(BsonType.ObjectId)]
        public string UploadedById { get; set; } = null!;
        
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
        
        // Soft delete flag as per guidelines
        public bool IsDeleted { get; set; } = false;
    }
}