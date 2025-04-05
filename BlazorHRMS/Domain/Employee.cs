using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BlazorHRMS.Domain
{
    /// <summary>
    /// Represents an employee in the HR Management System.
    /// Core entity in the Employee module following DDD principles.
    /// </summary>
    public class Employee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;
        
        public string EmployeeCode { get; set; } = null!;
        
        public string FirstName { get; set; } = null!;
        
        public string LastName { get; set; } = null!;
        
        public string? MiddleName { get; set; }
        
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime DateOfBirth { get; set; }
        
        public string Email { get; set; } = null!;
        
        public string Phone { get; set; } = null!;
        
        public Address Address { get; set; } = null!;
        
        public EmploymentDetails EmploymentDetails { get; set; } = null!;
        
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? UpdatedAt { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        // Soft delete flag as per guidelines
        public bool IsDeleted { get; set; } = false;
    }
    
    public class Address
    {
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
        public string Country { get; set; } = null!;
    }
    
    public class EmploymentDetails
    {
        public string Department { get; set; } = null!;
        public string Position { get; set; } = null!;
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime JoinDate { get; set; }
        public string EmploymentType { get; set; } = null!; // Full-time, Part-time, Contract
        public string? ReportsTo { get; set; } // Manager's Employee ID
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? TerminationDate { get; set; }
        public decimal Salary { get; set; }
    }
}