using Microsoft.EntityFrameworkCore;
using Northwind.Lib.CommonData;
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
        private readonly NorthWindRepository<Shipper> _repo;

        public ShipperServices(NorthWindRepository<Shipper> repo)
        {
            _repo = repo;
        }

        public async Task<ServiceResponse<bool>> AddShipper(Shipper shipper)
        {
            _repo.AddAsync(shipper);
            var result = await _repo.SaveChangesAsync();
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
            await _repo.DeleteAsync(shipper);
            await _repo.SaveChangesAsync();
        }

        public async Task<List<Shipper>> GetShipper()
        {
            var shipper = await _repo.ListAsync();
            return shipper;
        }

        public async Task<Shipper> GetShipperById(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task UpdateShipper(Shipper shipper)
        {
            await _repo.UpdateAsync(shipper);
        }
    }
}
