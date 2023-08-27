using Microsoft.EntityFrameworkCore;
using NorthWind.DAL;

namespace NorthWind.Services
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAllEmployees();
        Task<Employee> GetEmployee(int id);
        Task UpdateEmployee (Employee employee);
        Task DeleteEmployee (Employee employee);
    }

    public class EmployeeService : IEmployeeService
    {
        private readonly NorthWindContext _dbContext;
        public EmployeeService(NorthWindContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Employee> GetEmployee(int id)
        {
            return await _dbContext.Employees.FirstOrDefaultAsync(x => x.EmployeeId == id);
        }

        public async Task UpdateEmployee(Employee employee)
        {
            _dbContext.Employees.Update(employee);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteEmployee(Employee employee)
        {
            _dbContext.Employees.Remove(employee);
            await _dbContext.SaveChangesAsync();

        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            var employees = await _dbContext.Employees.ToListAsync();

            return employees;
        }
    }
}