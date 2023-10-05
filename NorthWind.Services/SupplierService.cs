using Microsoft.EntityFrameworkCore;
using Northwind.Lib.CommonData;
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
        private readonly NorthWindRepository<Supplier> _repo;

        public SupplierService(NorthWindRepository<Supplier> repo)
        {
            _repo = repo;
        }

        public async Task<List<Supplier>> GetSupplier()
        {
            var supplier = await _repo.ListAsync();
            return supplier;
        }

        public Task<Supplier> GetSupplierById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
