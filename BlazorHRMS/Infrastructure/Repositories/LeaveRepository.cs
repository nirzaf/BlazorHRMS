using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorHRMS.Domain;
using BlazorHRMS.Services;
using MongoDB.Driver;

namespace BlazorHRMS.Infrastructure.Repositories
{
    /// <summary>
    /// Repository implementation for LeaveRequest entity operations
    /// </summary>
    public class LeaveRequestRepository : BaseRepository<LeaveRequest>, ILeaveRequestRepository
    {
        private readonly IMongoDBService _mongoDBService;
        
        public LeaveRequestRepository(IMongoDBService mongoDBService) 
            : base(mongoDBService, "leaveRequests")
        {
            _mongoDBService = mongoDBService;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<LeaveRequest>> GetLeaveRequestsByEmployeeAsync(string employeeId)
        {
            var filter = Builders<LeaveRequest>.Filter.And(
                Builders<LeaveRequest>.Filter.Eq(l => l.EmployeeId, employeeId),
                Builders<LeaveRequest>.Filter.Eq(l => l.IsDeleted, false)
            );
            
            return await Collection.Find(filter).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<LeaveRequest>> GetLeaveRequestsByStatusAsync(string status)
        {
            var filter = Builders<LeaveRequest>.Filter.And(
                Builders<LeaveRequest>.Filter.Eq(l => l.Status, status),
                Builders<LeaveRequest>.Filter.Eq(l => l.IsDeleted, false)
            );
            
            return await Collection.Find(filter).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<LeaveRequest>> GetLeaveRequestsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var filter = Builders<LeaveRequest>.Filter.And(
                Builders<LeaveRequest>.Filter.Gte(l => l.StartDate, startDate),
                Builders<LeaveRequest>.Filter.Lte(l => l.EndDate, endDate),
                Builders<LeaveRequest>.Filter.Eq(l => l.IsDeleted, false)
            );
            
            return await Collection.Find(filter).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<LeaveRequest>> GetPendingLeaveRequestsForManagerAsync(string managerId)
        {
            // Get all employees that report to this manager
            var employeeCollection = _mongoDBService.GetCollection<Employee>("employees");
            var employeeFilter = Builders<Employee>.Filter.And(
                Builders<Employee>.Filter.Eq(e => e.EmploymentDetails.ReportsTo, managerId),
                Builders<Employee>.Filter.Eq(e => e.IsDeleted, false)
            );
            
            var employees = await employeeCollection.Find(employeeFilter).ToListAsync();
            var employeeIds = employees.Select(e => e.Id).ToList();
            
            // Get pending leave requests for these employees
            var leaveFilter = Builders<LeaveRequest>.Filter.And(
                Builders<LeaveRequest>.Filter.In(l => l.EmployeeId, employeeIds),
                Builders<LeaveRequest>.Filter.Eq(l => l.Status, "Submitted"),
                Builders<LeaveRequest>.Filter.Eq(l => l.IsDeleted, false)
            );
            
            return await Collection.Find(leaveFilter).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateLeaveRequestStatusAsync(string id, string status, string approverId, string? comments)
        {
            var filter = Builders<LeaveRequest>.Filter.Eq(l => l.Id, id);
            
            var updateDefinition = Builders<LeaveRequest>.Update
                .Set(l => l.Status, status)
                .Set(l => l.ApprovedById, approverId)
                .Set(l => l.ApprovedAt, DateTime.UtcNow)
                .Set(l => l.UpdatedAt, DateTime.UtcNow);
                
            if (!string.IsNullOrEmpty(comments))
            {
                updateDefinition = updateDefinition.Set(l => l.RejectionReason, comments);
            }
            
            var result = await Collection.UpdateOneAsync(filter, updateDefinition);
            return result.ModifiedCount > 0;
        }
    }
    
    /// <summary>
    /// Repository implementation for LeaveType entity operations
    /// </summary>
    public class LeaveTypeRepository : BaseRepository<LeaveType>, ILeaveTypeRepository
    {
        public LeaveTypeRepository(IMongoDBService mongoDBService) 
            : base(mongoDBService, "leaveTypes")
        {
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<LeaveType>> GetActiveLeaveTypesAsync()
        {
            var filter = Builders<LeaveType>.Filter.Eq(l => l.IsActive, true);
            return await Collection.Find(filter).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<LeaveType> GetLeaveTypeByNameAsync(string name)
        {
            var filter = Builders<LeaveType>.Filter.Eq(l => l.Name, name);
            return await Collection.Find(filter).FirstOrDefaultAsync();
        }
    }
    
    /// <summary>
    /// Repository implementation for LeaveBalance entity operations
    /// </summary>
    public class LeaveBalanceRepository : BaseRepository<LeaveBalance>, ILeaveBalanceRepository
    {
        public LeaveBalanceRepository(IMongoDBService mongoDBService) 
            : base(mongoDBService, "leaveBalances")
        {
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<LeaveBalance>> GetLeaveBalancesByEmployeeAsync(string employeeId)
        {
            var filter = Builders<LeaveBalance>.Filter.Eq(l => l.EmployeeId, employeeId);
            return await Collection.Find(filter).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<LeaveBalance> GetLeaveBalanceAsync(string employeeId, string leaveTypeId, int year)
        {
            var filter = Builders<LeaveBalance>.Filter.And(
                Builders<LeaveBalance>.Filter.Eq(l => l.EmployeeId, employeeId),
                Builders<LeaveBalance>.Filter.Eq(l => l.LeaveTypeId, leaveTypeId),
                Builders<LeaveBalance>.Filter.Eq(l => l.Year, year)
            );
            
            return await Collection.Find(filter).FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateLeaveBalanceAsync(LeaveBalance leaveBalance)
        {
            leaveBalance.UpdatedAt = DateTime.UtcNow;
            
            var filter = Builders<LeaveBalance>.Filter.Eq(l => l.Id, leaveBalance.Id);
            var result = await Collection.ReplaceOneAsync(filter, leaveBalance);
            
            return result.ModifiedCount > 0;
        }
    }
}