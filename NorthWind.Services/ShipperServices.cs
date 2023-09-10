using Microsoft.EntityFrameworkCore;
using NorthWind.DAL;
using NorthWind.Services.Models;

namespace NorthWind.Services
{
    public interface IShipperServices
    {
        Task<List<Shipper>> GetShipper();
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
            _dbContext.Shippers.Add(shipper);
            var result = await _dbContext.SaveChangesAsync();
            var isSuccessful = result > 0;

            var resultModel = new ServiceResponse<bool>
            {
                IsSuccessful = isSuccessful,
                Message = "Added Shipper"
            };

            return resultModel;
            
        }

        public async Task DeleteShipper(Shipper shipper)
        {
           _dbContext.Shippers.Remove(shipper);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Shipper>> GetShipper()
        {
            var shipper = await _dbContext.Shippers.ToListAsync();
            return shipper;
        }

        public async Task<Shipper> GetShipperById(int id)
        {
            var shipper = await _dbContext.Shippers.FirstOrDefaultAsync(x => x.ShipperId == id);
            return shipper;
        }

        public async Task UpdateShipper(Shipper shipper)
        {
            _dbContext.Shippers.Update(shipper);
            await _dbContext.SaveChangesAsync();
        }
    }
}
