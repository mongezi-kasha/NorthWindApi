using Microsoft.AspNetCore.Mvc;
using Northwind.Models.Invoices;
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
            var invoice = await _invoiceService.GetInvoice(invoiceId);
            return Ok(invoice);
        }

        [HttpPost]
        public async Task<IActionResult> AddInvoce([FromBody] Invoice invoice)
        {
            var invoices = await _invoiceService.AddInvoce(invoice);
            return Ok(invoices);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateInvoice([FromBody] UpdateInvoiceRequest invoiceRequest)
        {
            var invoice = await _invoiceService.GetInvoice(invoiceRequest.InvoiceId);
            if (invoice == null)
            {
                return NotFound();
            }

            invoice.ShipName = invoiceRequest.InvoiceName;

            await _invoiceService.UpdateInvoice(invoice);

            return Ok(invoice);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteInvoice([FromBody] DeleteInvoiceRequest deleteRequest)
        {
            var invoice = await _invoiceService.GetInvoice(deleteRequest.InvoiceId);
            if (invoice == null)
            {
                return NotFound();
            }

            await _invoiceService.DeleteInvoice(invoice);

            return Ok(invoice);
        }
    }
}
