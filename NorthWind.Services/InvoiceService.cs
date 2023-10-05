using Microsoft.EntityFrameworkCore;
using Northwind.Lib.CommonData;
using NorthWind.DAL;
using NorthWind.Services.Models;

namespace NorthWind.Services
{
    public interface IInvoiceService
    {
        Task<List<Invoice>> GetAllInvoices();
        Task <Invoice> GetInvoice(int id);
        Task UpdateInvoice (Invoice invoice);
        Task DeleteInvoice (Invoice invoice);
        Task<ServiceResponse<bool>> AddInvoce(Invoice invoice);
    }

    public class InvoiceService : IInvoiceService
    {
        private readonly NorthWindRepository<Invoice> _repo;

        public InvoiceService(NorthWindRepository<Invoice> repo)
        {
            _repo = repo;
        }

        public async Task<Invoice> GetInvoice(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task UpdateInvoice(Invoice invoice)
        {
            await _repo.UpdateAsync(invoice);
        }

        public async Task<List<Invoice>> GetAllInvoices()
        {
            var invoices = await _repo.ListAsync();
            return invoices;
        }

        public async Task DeleteInvoice(Invoice invoice)
        {
            await _repo.DeleteAsync(invoice);
            await _repo.SaveChangesAsync();

        }

        public async Task<ServiceResponse<bool>> AddInvoce(Invoice invoice)
        {
            _repo.AddAsync(invoice);
            var result = await _repo.SaveChangesAsync();
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
