using Microsoft.AspNetCore.Mvc;
using Northwind.Models.Shipper;
using NorthWind.DAL;
using NorthWind.Services;
using NorthWind.Services.Models;

namespace Northwind.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ShipperController : ControllerBase
    {
        private readonly ShipperServices _shipperService;
        public ShipperController(ShipperServices shipperService)
        {
            _shipperService = shipperService;
        }
        [HttpGet]
        public Task<ServiceResponse<List<Shipper>>> GetShipper()
        {
            var shipper = _shipperService.GetShipper();
            return shipper;
        }
        [HttpGet("{id}")]
        public async Task<Shipper> GetShipperById(int id)
        {
            var shipper = await _shipperService.GetShipperById(id);
            return shipper;
        }
        [HttpPut]
        public async Task<IActionResult> UpdateShipper([FromBody] UpdateShipperRequest updateShipperRequest)
        {
            var shipper = await _shipperService.GetShipperById(updateShipperRequest.ShipperId);
            if(shipper == null)
            {
                return NotFound();
            }

            shipper.CompanyName = updateShipperRequest.ShipperName;
            await _shipperService.UpdateShipper(shipper);
            return Ok(shipper);
        }

        [HttpDelete]

        public async Task<IActionResult> DeleteShipper([FromBody] DeleteShipperRequest deleteShipperRequest)
        {
            var shipper = await _shipperService.GetShipperById(deleteShipperRequest.ShipperId);
            if(shipper == null)
            {
                return NotFound();
            }

            await _shipperService.DeleteShipper(shipper);
            return Ok(shipper);
        }
    }
}
