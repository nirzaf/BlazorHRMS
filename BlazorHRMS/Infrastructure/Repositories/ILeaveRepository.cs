using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorHRMS.Domain;

namespace BlazorHRMS.Infrastructure.Repositories
{
    /// <summary>
    /// Repository interface for LeaveRequest entity operations
    /// </summary>
    public interface ILeaveRequestRepository : IRepository<LeaveRequest>
    {
        /// <summary>
        /// Gets leave requests by employee ID
        /// </summary>
        /// <param name="employeeId">The employee ID</param>
        /// <returns>A collection of leave requests for the specified employee</returns>
        Task<IEnumerable<LeaveRequest>> GetLeaveRequestsByEmployeeAsync(string employeeId);
        
        /// <summary>
        /// Gets leave requests by status
        /// </summary>
        /// <param name="status">The leave request status</param>
        /// <returns>A collection of leave requests with the specified status</returns>
        Task<IEnumerable<LeaveRequest>> GetLeaveRequestsByStatusAsync(string status);
        
        /// <summary>
        /// Gets leave requests for a specific date range
        /// </summary>
        /// <param name="startDate">The start date</param>
        /// <param name="endDate">The end date</param>
        /// <returns>A collection of leave requests within the specified date range</returns>
        Task<IEnumerable<LeaveRequest>> GetLeaveRequestsByDateRangeAsync(DateTime startDate, DateTime endDate);
        
        /// <summary>
        /// Gets pending leave requests for a manager to approve
        /// </summary>
        /// <param name="managerId">The manager's employee ID</param>
        /// <returns>A collection of pending leave requests for the manager's team</returns>
        Task<IEnumerable<LeaveRequest>> GetPendingLeaveRequestsForManagerAsync(string managerId);
        
        /// <summary>
        /// Updates the status of a leave request
        /// </summary>
        /// <param name="id">The leave request ID</param>
        /// <param name="status">The new status</param>
        /// <param name="approverId">The ID of the employee who approved/rejected the request</param>
        /// <param name="comments">Optional comments</param>
        /// <returns>True if the update was successful, false otherwise</returns>
        Task<bool> UpdateLeaveRequestStatusAsync(string id, string status, string approverId, string? comments);
    }
    
    /// <summary>
    /// Repository interface for LeaveType entity operations
    /// </summary>
    public interface ILeaveTypeRepository : IRepository<LeaveType>
    {
        /// <summary>
        /// Gets active leave types
        /// </summary>
        /// <returns>A collection of active leave types</returns>
        Task<IEnumerable<LeaveType>> GetActiveLeaveTypesAsync();
        
        /// <summary>
        /// Gets a leave type by name
        /// </summary>
        /// <param name="name">The leave type name</param>
        /// <returns>The leave type if found, null otherwise</returns>
        Task<LeaveType> GetLeaveTypeByNameAsync(string name);
    }
    
    /// <summary>
    /// Repository interface for LeaveBalance entity operations
    /// </summary>
    public interface ILeaveBalanceRepository : IRepository<LeaveBalance>
    {
        /// <summary>
        /// Gets leave balances for an employee
        /// </summary>
        /// <param name="employeeId">The employee ID</param>
        /// <returns>A collection of leave balances for the specified employee</returns>
        Task<IEnumerable<LeaveBalance>> GetLeaveBalancesByEmployeeAsync(string employeeId);
        
        /// <summary>
        /// Gets a specific leave balance for an employee and leave type
        /// </summary>
        /// <param name="employeeId">The employee ID</param>
        /// <param name="leaveTypeId">The leave type ID</param>
        /// <param name="year">The year</param>
        /// <returns>The leave balance if found, null otherwise</returns>
        Task<LeaveBalance> GetLeaveBalanceAsync(string employeeId, string leaveTypeId, int year);
        
        /// <summary>
        /// Updates a leave balance
        /// </summary>
        /// <param name="leaveBalance">The updated leave balance</param>
        /// <returns>True if the update was successful, false otherwise</returns>
        Task<bool> UpdateLeaveBalanceAsync(LeaveBalance leaveBalance);
    }
}