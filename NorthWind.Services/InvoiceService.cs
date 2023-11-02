using Microsoft.EntityFrameworkCore;
using Northwind.Lib.CommonData;
using NorthWind.DAL;
using NorthWind.Services.Models;

namespace NorthWind.Services
{
    public interface IInvoiceService
    {
        Task<ServiceResponse<List<Invoice>>> GetAllInvoices();
        Task<Invoice> GetInvoice(int id);
        Task UpdateInvoice(Invoice invoice);
        Task DeleteInvoice(Invoice invoice);
        Task<ServiceResponse<bool>> AddInvoce(Invoice invoice);
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

        public async Task<ServiceResponse<List<Invoice>>> GetAllInvoices()
        {
            var invoices = await _dbContext.Invoices.ToListAsync();
            return new ServiceResponse<List<Invoice>> { Data = invoices, IsSuccessful = true, Message = "Success" };
        }

        public async Task DeleteInvoice(Invoice invoice)
        {
            _dbContext.Invoices.Remove(invoice);
                await _dbContext.SaveChangesAsync();
        }

        public async Task<ServiceResponse<bool>> AddInvoce(Invoice invoice)
        {
            _dbContext.Invoices.Add(invoice);
            var result = await _dbContext.SaveChangesAsync();
            var isSuccessful = result > 0;

            var resultModel = new ServiceResponse<bool>
            {
                IsSuccessful = isSuccessful,
                Message = "Successfully added an Invoice"
            };

            return resultModel;
        }
    }
}
