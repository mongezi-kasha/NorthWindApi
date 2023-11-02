using Microsoft.EntityFrameworkCore;
using Northwind.Lib.CommonData;
using NorthWind.DAL;
using NorthWind.Services.Models;

namespace NorthWind.Services
{
    //Still to add stuff
    public interface ISupplierService
    {
        Task<ServiceResponse<List<Supplier>>> GetSupplier();
        Task<Supplier> GetSupplierById(int id);
    }
    public class SupplierService : ISupplierService
    {
        private readonly NorthWindContext _dbContext;

        public SupplierService(NorthWindContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ServiceResponse<List<Supplier>>> GetSupplier()
        {
           var suppliers = await _dbContext.Suppliers.ToListAsync();
            return new ServiceResponse<List<Supplier>> { Data = suppliers, IsSuccessful = true, Message = "Success" };
        }

        public Task<Supplier> GetSupplierById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
