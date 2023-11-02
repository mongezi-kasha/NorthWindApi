using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Northwind.Lib.CommonData;
using NorthWind.DAL;
using NorthWind.Services.Models;

namespace NorthWind.Services
{
    public interface IShipperServices
    {
        Task<ServiceResponse<List<Shipper>>> GetShipper();
        Task <Shipper> GetShipperById(int id);
        Task UpdateShipper (Shipper shipper); 
        Task DeleteShipper(Shipper shipper);
        Task<ServiceResponse<bool>> AddShipper(Shipper shipper);
    }
    public class ShipperServices : IShipperServices
    {
        private readonly NorthWindContext _dbContext;

        public ShipperServices(NorthWindContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ServiceResponse<bool>> AddShipper(Shipper shipper)
        {
            _dbContext.Shippers.AddAsync(shipper);
            var result = await _dbContext.SaveChangesAsync();
            var isSuccessful = result > 0;

            var resultModel = new ServiceResponse<bool>
            {
                IsSuccessful = isSuccessful,
                Message = "Successfully added a Shipper"
            };
            
            return resultModel;
        }

        public async Task DeleteShipper(Shipper shipper)
        {
             _dbContext.Shippers.Remove(shipper);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ServiceResponse<List<Shipper>>> GetShipper()
        {
            var employees = await _dbContext.Shippers.ToListAsync();
            return new ServiceResponse<List<Shipper>> { Data = employees, IsSuccessful = true, Message = "Success" };
        }

        public async Task<Shipper> GetShipperById(int id)
        {
            return await _dbContext.Shippers.FirstOrDefaultAsync(x => x.ShipperId == id);
        }

        public async Task UpdateShipper(Shipper shipper)
        {
            _dbContext.Shippers.Update(shipper);
            await _dbContext.SaveChangesAsync();
        }
    }
}
