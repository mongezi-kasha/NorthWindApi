using Microsoft.EntityFrameworkCore;
using NorthWind.DAL;

namespace NorthWind.Services
{
    public interface IInvoiceService
    {
        Task<List<Invoice>> GetAllInvoices();
    }

    public class InvoiceService : IInvoiceService
    {
        private readonly NorthWindContext _dbContext;
        public InvoiceService(NorthWindContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Invoice>> GetAllInvoices()
        {
            var employee = await _dbContext.Invoices.ToListAsync();
            return employee;
        }
    }
}
