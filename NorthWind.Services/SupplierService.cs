using Microsoft.EntityFrameworkCore;
using NorthWind.DAL;

namespace NorthWind.Services
{
    public interface ISupplierService
    {
        Task<List<Supplier>> GetSupplier();
        Task<Supplier> GetSupplierById(int id);
    }
    public class SupplierService : ISupplierService
    {
        private readonly NorthWindContext _dbContext;
        public SupplierService(NorthWindContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<Supplier>> GetSupplier()
        {
            var suppliers = _dbContext.Suppliers.ToListAsync();
            return suppliers;
        }

        public Task<Supplier> GetSupplierById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
