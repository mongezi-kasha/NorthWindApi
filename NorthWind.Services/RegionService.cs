using Microsoft.EntityFrameworkCore;
using Northwind.Lib.CommonData;
using NorthWind.DAL;
using NorthWind.Services.Models;

namespace NorthWind.Services
{
   
    public interface IRegionService
    {
        Task<List<Region>> GetRegion();
        Task <Region> GetRegionById(int id);
        Task<ServiceResponse<bool>> AddRegion(Region region);
        Task UpdateRegion(Region region);
        Task DeleteRegion(Region region);
    }
    public class RegionService : IRegionService
    {
        private readonly NorthWindRepository<Region> _repo;

        public RegionService(NorthWindRepository<Region> repo)
        {
            _repo = repo;
        }

        public async Task<ServiceResponse<bool>> AddRegion(Region region)
        {
            _repo.AddAsync(region);
            var result = await _repo.SaveChangesAsync();
            var isSuccessfull = result > 0;

            var resultModel = new ServiceResponse<bool>
            {
                IsSuccessful = isSuccessfull,
                Message = "Added a Region"
            };

            return resultModel;
        }

        public async Task DeleteRegion(Region region)
        {
            await _repo.DeleteAsync(region);
            await _repo.SaveChangesAsync();
        }

        public async Task<List<Region>> GetRegion()
        {
            var region = await _repo.ListAsync();
            return region;
        }

        public async Task<Region> GetRegionById(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task UpdateRegion(Region region)
        {
            await _repo.UpdateAsync(region);
        }
    }
}
