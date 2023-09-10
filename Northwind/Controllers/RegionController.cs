using Microsoft.AspNetCore.Mvc;
using NorthWind.DAL;
using NorthWind.Services;

namespace Northwind.Controllers
{
    [Route("api/[controller")]
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
    }
}
