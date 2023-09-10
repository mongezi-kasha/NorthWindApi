using Microsoft.AspNetCore.Mvc;
using NorthWind.DAL;
using NorthWind.Services;

namespace Northwind.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly SupplierService _supplierService;
        
        public SupplierController(SupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        public async Task<List<Supplier>> GetSupplier()
        {
            var supplier = await _supplierService.GetSupplier();
            return supplier;
        }
    }
}
