using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthWind.DAL;
using NorthWind.Services;

namespace Northwind.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly NorthWindContext _dbContext;
        private readonly IInvoiceService _invoiceService;
        public InvoiceController(NorthWindContext dbContext, IInvoiceService invoiceService)
        {
            _dbContext = dbContext;
            _invoiceService = invoiceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetInvoices()
        {
            var invoices = await _invoiceService.GetAllInvoices();
            return Ok(invoices);
        }

        [HttpGet("{invoiceId}")]
        public async Task<IActionResult> GetInvoice([FromQuery] int invoiceId)
        {
            var invoice = await _dbContext.Invoices.Where(x => x.invoiceId == invoiceId).FirstOrDefaultAsync();
            return Ok(invoice);
        }

        [HttpPost]
        public async Task<IActionResult> AddInvoce([FromBody] Invoice invoice)
        {
            await _dbContext.Invoices.AddAsync(invoice);
            var result = await _dbContext.SaveChangesAsync();
            var isSuccessfull = result > 0;

            var resultmodel = new
            {
                Success = isSuccessfull,
                Message = "Successfully added invoice"
            };

            return Ok(resultmodel);
        }
    }
}
