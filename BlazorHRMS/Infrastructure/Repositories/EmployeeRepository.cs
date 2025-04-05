using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorHRMS.Domain;
using BlazorHRMS.Services;
using MongoDB.Driver;

namespace BlazorHRMS.Infrastructure.Repositories
{
    /// <summary>
    /// Repository implementation for Employee entity operations
    /// </summary>
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IMongoDBService mongoDBService) 
            : base(mongoDBService, "employees")
        {
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Employee>> GetEmployeesByDepartmentAsync(string department)
        {
            var filter = Builders<Employee>.Filter.And(
                Builders<Employee>.Filter.Eq(e => e.EmploymentDetails.Department, department),
                Builders<Employee>.Filter.Eq(e => e.IsDeleted, false)
            );
            
            return await Collection.Find(filter).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Employee>> GetEmployeesByManagerAsync(string managerId)
        {
            var filter = Builders<Employee>.Filter.And(
                Builders<Employee>.Filter.Eq(e => e.EmploymentDetails.ReportsTo, managerId),
                Builders<Employee>.Filter.Eq(e => e.IsDeleted, false)
            );
            
            return await Collection.Find(filter).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<Employee> GetEmployeeByEmailAsync(string email)
        {
            var filter = Builders<Employee>.Filter.And(
                Builders<Employee>.Filter.Eq(e => e.Email, email),
                Builders<Employee>.Filter.Eq(e => e.IsDeleted, false)
            );
            
            return await Collection.Find(filter).FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public async Task<Employee> GetEmployeeByCodeAsync(string employeeCode)
        {
            var filter = Builders<Employee>.Filter.And(
                Builders<Employee>.Filter.Eq(e => e.EmployeeCode, employeeCode),
                Builders<Employee>.Filter.Eq(e => e.IsDeleted, false)
            );
            
            return await Collection.Find(filter).FirstOrDefaultAsync();
        }
    }
}