using Microsoft.EntityFrameworkCore;
using NorthWind.DAL;

namespace NorthWind.Services
{
    public interface IInvoiceService
    {
        Task<List<Invoice>> GetAllInvoices();
        Task <Invoice> GetInvoice(int id);
        Task UpdateInvoice (Invoice invoice);
        Task DeleteInvoice (Invoice invoice);
    }

    public class InvoiceService : IInvoiceService
    {
        private readonly NorthWindContext _dbContext;
        public InvoiceService(NorthWindContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Invoice> GetInvoice(int id)
        {
            return await _dbContext.Invoices.FirstOrDefaultAsync(x => x.invoiceId == id);
        }

        public async Task UpdateInvoice(Invoice invoice)
        {
            _dbContext.Invoices.Update(invoice);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Invoice>> GetAllInvoices()
        {
            var employee = await _dbContext.Invoices.ToListAsync();
            return employee;
        }

        public async Task DeleteInvoice(Invoice invoice)
        {
            _dbContext.Invoices.Remove(invoice);
            await _dbContext.SaveChangesAsync();

        }
    }
}
