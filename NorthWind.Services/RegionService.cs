using Microsoft.EntityFrameworkCore;
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
        private readonly NorthWindContext _dbContext;
        public RegionService(NorthWindContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ServiceResponse<bool>> AddRegion(Region region)
        {
            _dbContext.Regions.Add(region);
            var result = await _dbContext.SaveChangesAsync();
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
            _dbContext.Regions.Remove(region);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Region>> GetRegion()
        {
            var regions = await _dbContext.Regions.ToListAsync();
            return regions;
        }

        public async Task<Region> GetRegionById(int id)
        {
            var regions = await _dbContext.Regions.FirstOrDefaultAsync(x => x.RegionId == id);
            return regions;
        }

        public async Task UpdateRegion(Region region)
        {
            _dbContext.Regions.Update(region);
            await _dbContext.SaveChangesAsync();
        }
    }
}
