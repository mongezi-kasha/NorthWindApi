using Microsoft.EntityFrameworkCore;
using NorthWind.DAL;

namespace NorthWind.Services
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAllEmployees();
    }

    public class EmployeeService : IEmployeeService
    {
        private readonly NorthWindContext _dbContext;
        public EmployeeService(NorthWindContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            var employees = await _dbContext.Employees.ToListAsync();

            return employees;
        }
    }

    public class DummyEmployeeService : IEmployeeService
    {
        public Task<List<Employee>> GetAllEmployees()
        {
            var list = new List<Employee>();
            var employee = new Employee
            {
                FirstName = "Mongezi",
                LastName = "Kasha"
            };

            list.Add(employee);
            return Task.FromResult(list);
            
        }
    }
}