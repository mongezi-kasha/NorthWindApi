using Microsoft.EntityFrameworkCore;
using NorthWind.DAL;
using NorthWind.Services.Models;

namespace NorthWind.Services
{
    public interface IEmployeeService
    {
        Task<ServiceResponse<bool>> AddEmployee(Employee employee);
        Task DeleteEmployee(Employee employee);
        Task<ServiceResponse<List<Employee>>> GetAllEmployees();
        Task<ServiceResponse<Employee>> GetEmployee(int id);
        Task UpdateEmployee(Employee employee);
    }

    public class EmployeeService : IEmployeeService
    {
        private readonly NorthWindContext _dbContext;
        public EmployeeService(NorthWindContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ServiceResponse<Employee>> GetEmployee(int id)
        {
            var result = await _dbContext.Employees.FirstOrDefaultAsync(x => x.EmployeeId == id);
            return new ServiceResponse<Employee> { Data = result, IsSuccessful = result is null ? false : true };
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

        public async Task<ServiceResponse<List<Employee>>> GetAllEmployees()
        {
            var employees = await _dbContext.Employees.ToListAsync();

            return new ServiceResponse<List<Employee>> { Data = employees, IsSuccessful = true, Message = "Success" };
        }

        public async Task<ServiceResponse<bool>> AddEmployee(Employee employee)
        {
            _dbContext.Employees.Add(employee);
            var result = await _dbContext.SaveChangesAsync();
            var isSuccessful = result > 0;

            var resultModel = new ServiceResponse<bool>
            {
                IsSuccessful = isSuccessful,
                Message = "Successfully added employee"
            };

            return resultModel;
        }
    }
}