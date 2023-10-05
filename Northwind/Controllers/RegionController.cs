using Microsoft.AspNetCore.Mvc;
using Northwind.Models.Region;
using NorthWind.DAL;
using NorthWind.Services;

namespace Northwind.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IRegionService _regionService;

        public RegionController(IRegionService regionService)
        {
            _regionService = regionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRegion()
        {
            var region = await _regionService.GetRegion();
            return Ok(region);
        }

        [HttpGet("RegionId")]
        public async Task<IActionResult> GetRegionById(int id)
        {
            var region = await _regionService.GetRegionById(id);
            return Ok(region);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegion([FromBody] Region region)
        {
            var regions = await _regionService.AddRegion(region);
            return Ok(regions);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRegion([FromBody] UpdateRegionrRequest updateRegionRequest)
        {
            var regions = await _regionService.GetRegionById(updateRegionRequest.RegionId);
            if (regions == null)
            {
                return NotFound();
            }

            regions.RegionDescription = updateRegionRequest.RegionName;

            await _regionService.UpdateRegion(regions);

            return Ok(regions);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRegion([FromBody] DeleteRegionRequest deleteRegionRequest)
        {
            var region = await _regionService.GetRegionById(deleteRegionRequest.RegionID);
            if (region == null)
            {
                return NotFound();
            }

            await _regionService.DeleteRegion(region);
            return Ok(region);
        }
    }
}
