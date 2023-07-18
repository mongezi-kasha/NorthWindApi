using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthWind.DAL;
using Microsoft.EntityFrameworkCore;
using NorthWind.Services;

namespace Northwind.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly NorthWindContext _dbContext;
        private readonly IEmployeeService _employeeService;
        public EmployeeController(NorthWindContext dbContext, IEmployeeService employeeService)
        {
            _dbContext = dbContext;
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _employeeService.GetAllEmployees();

            return Ok(employees);
        }

        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetEmployee([FromQuery] int EmployeeId)
        {
            var employee = await _dbContext.Employees.Where(x => x.EmployeeId == EmployeeId).FirstOrDefaultAsync();

            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
        {
            await _dbContext.Employees.AddAsync(employee);
            var result = await _dbContext.SaveChangesAsync();
            var isSuccessful = result > 0;

            var resultModel = new
            {
                Success = isSuccessful,
                Message = "Successfully added employee"
            };

            return Ok(resultModel);
        }
    }
}
