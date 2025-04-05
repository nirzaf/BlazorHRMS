using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BlazorHRMS.Domain
{
    /// <summary>
    /// Represents a performance review for an employee
    /// Core entity in the Performance module following DDD principles.
    /// </summary>
    public class PerformanceReview
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        
        [BsonRepresentation(BsonType.ObjectId)]
        public string EmployeeId { get; set; } = null!;
        
        [BsonRepresentation(BsonType.ObjectId)]
        public string ReviewTemplateId { get; set; } = null!;
        
        public string ReviewPeriod { get; set; } = null!; // e.g., "2025 Q1", "2025 Annual"
        
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime StartDate { get; set; }
        
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime EndDate { get; set; }
        
        // Status: Draft, SelfReviewCompleted, ManagerReviewCompleted, Completed
        public string Status { get; set; } = "Draft";
        
        [BsonRepresentation(BsonType.ObjectId)]
        public string ReviewerId { get; set; } = null!; // Usually the manager
        
        public List<SectionResponse> SectionResponses { get; set; } = new List<SectionResponse>();
        
        public decimal OverallRating { get; set; }
        
        public string OverallComments { get; set; } = null!;
        
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? CompletedDate { get; set; }
        
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? UpdatedAt { get; set; }
        
        // Soft delete flag as per guidelines
        public bool IsDeleted { get; set; } = false;
    }
    
    /// <summary>
    /// Represents a template for performance reviews
    /// </summary>
    public class ReviewTemplate
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        
        public string Name { get; set; } = null!;
        
        public string Description { get; set; } = null!;
        
        public List<ReviewSection> Sections { get; set; } = new List<ReviewSection>();
        
        public bool IsActive { get; set; } = true;
        
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? UpdatedAt { get; set; }
    }
    
    /// <summary>
    /// Represents a section in a review template
    /// </summary>
    public class ReviewSection
    {
        public string Title { get; set; } = null!;
        
        public string Description { get; set; } = null!;
        
        public int Weight { get; set; } // Percentage weight in the overall review
        
        public List<ReviewQuestion> Questions { get; set; } = new List<ReviewQuestion>();
    }
    
    /// <summary>
    /// Represents a question in a review section
    /// </summary>
    public class ReviewQuestion
    {
        public string Question { get; set; } = null!;
        
        // Type: Rating, Text, MultipleChoice
        public string Type { get; set; } = null!;
        
        public List<string>? Options { get; set; } // For MultipleChoice type
        
        public bool RequiresComments { get; set; } = false;
    }
    
    /// <summary>
    /// Represents a response to a section in a performance review
    /// </summary>
    public class SectionResponse
    {
        public string SectionTitle { get; set; } = null!;
        
        public List<QuestionResponse> QuestionResponses { get; set; } = new List<QuestionResponse>();
        
        public decimal SectionRating { get; set; }
        
        public string SectionComments { get; set; } = null!;
    }
    
    /// <summary>
    /// Represents a response to a question in a performance review
    /// </summary>
    public class QuestionResponse
    {
        public string Question { get; set; } = null!;
        
        public string ResponseType { get; set; } = null!; // Rating, Text, MultipleChoice
        
        public decimal? RatingValue { get; set; }
        
        public string? TextValue { get; set; }
        
        public string? SelectedOption { get; set; }
        
        public string? Comments { get; set; }
    }
}