using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BlazorHRMS.Domain
{
    /// <summary>
    /// Represents a leave request in the HR Management System.
    /// Core entity in the Leave module following DDD principles.
    /// </summary>
    public class LeaveRequest
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        
        [BsonRepresentation(BsonType.ObjectId)]
        public string EmployeeId { get; set; } = null!;
        
        [BsonRepresentation(BsonType.ObjectId)]
        public string LeaveTypeId { get; set; } = null!;
        
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime StartDate { get; set; }
        
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime EndDate { get; set; }
        
        public decimal DurationInDays { get; set; }
        
        public string Reason { get; set; } = null!;
        
        // Workflow states: Submitted, Approved, Rejected, Cancelled
        public string Status { get; set; } = "Submitted";
        
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ApprovedById { get; set; }
        
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? ApprovedAt { get; set; }
        
        public string? RejectionReason { get; set; }
        
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? UpdatedAt { get; set; }
        
        // Soft delete flag as per guidelines
        public bool IsDeleted { get; set; } = false;
    }
    
    /// <summary>
    /// Represents a type of leave (e.g., Annual, Sick, Maternity)
    /// </summary>
    public class LeaveType
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        
        public string Name { get; set; } = null!;
        
        public string Description { get; set; } = null!;
        
        public decimal DefaultAllocation { get; set; } // Default days per year
        
        public bool RequiresApproval { get; set; } = true;
        
        public bool IsActive { get; set; } = true;
        
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? UpdatedAt { get; set; }
    }
    
    /// <summary>
    /// Represents an employee's leave balance for a specific leave type
    /// </summary>
    public class LeaveBalance
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        
        [BsonRepresentation(BsonType.ObjectId)]
        public string EmployeeId { get; set; } = null!;
        
        [BsonRepresentation(BsonType.ObjectId)]
        public string LeaveTypeId { get; set; } = null!;
        
        public int Year { get; set; } // The year this balance applies to
        
        public decimal Allocated { get; set; } // Total allocated days
        
        public decimal Used { get; set; } // Days used
        
        public decimal Pending { get; set; } // Days pending approval
        
        public decimal Remaining { get; set; } // Remaining days
        
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime LastAccrualDate { get; set; } // Date of last balance update
        
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? UpdatedAt { get; set; }
    }
}