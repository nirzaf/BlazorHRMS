using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorHRMS.Domain;

namespace BlazorHRMS.Infrastructure.Repositories
{
    /// <summary>
    /// Repository interface for Employee entity operations
    /// </summary>
    public interface IEmployeeRepository : IRepository<Employee>
    {
        /// <summary>
        /// Gets employees by department
        /// </summary>
        /// <param name="department">The department name</param>
        /// <returns>A collection of employees in the specified department</returns>
        Task<IEnumerable<Employee>> GetEmployeesByDepartmentAsync(string department);
        
        /// <summary>
        /// Gets employees by manager (reports to)
        /// </summary>
        /// <param name="managerId">The manager's employee ID</param>
        /// <returns>A collection of employees reporting to the specified manager</returns>
        Task<IEnumerable<Employee>> GetEmployeesByManagerAsync(string managerId);
        
        /// <summary>
        /// Gets an employee by email address
        /// </summary>
        /// <param name="email">The email address</param>
        /// <returns>The employee if found, null otherwise</returns>
        Task<Employee> GetEmployeeByEmailAsync(string email);
        
        /// <summary>
        /// Gets an employee by employee code
        /// </summary>
        /// <param name="employeeCode">The employee code</param>
        /// <returns>The employee if found, null otherwise</returns>
        Task<Employee> GetEmployeeByCodeAsync(string employeeCode);
    }
}