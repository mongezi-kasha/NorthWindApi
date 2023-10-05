using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using Northwind.Lib.CommonData;
using NorthWind.DAL;
using NorthWind.Services.Models;
using Specs = Northwind.Lib.CommonData.Specifications;

namespace NorthWind.Services
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllOrders();
        Task<Order> GetOrderById(int id);
        Task UpdateOrder(Order order);
        Task DeleteOrder(Order order);
        Task<ServiceResponse<bool>> AddOrder(Order order);
    }

    public class OrderService : IOrderService
    {
        private readonly NorthWindRepository<Order> _repo;

        public OrderService(NorthWindRepository<Order> repo)
        {
            _repo = repo;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            var orders = await _repo.ListAsync();
            return orders;
        }

        public async Task UpdateOrder(Order order)
        {
           await _repo.UpdateAsync(order);
        }

        public async Task<Order> GetOrderById(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<Order> GetOrderByCustomerId(string customerId)
        {
            return await _repo.FirstOrDefaultAsync(new Specs.OrderByCustomerIdSpec(customerId));
        }

        public async Task DeleteOrder(Order order)
        {
            await _repo.DeleteAsync(order);
            await _repo.SaveChangesAsync();

        }

        public async Task<ServiceResponse<bool>> AddOrder(Order order)
        {
            _repo.AddAsync(order);
            var result = await _repo.SaveChangesAsync();
            var isSuccessful = result > 0;

            var resultModel = new  ServiceResponse<bool>
            {
                IsSuccessful = isSuccessful,
                Message = "sUCCESSFULLY ADDED AN ODRER"
            };

            return resultModel;
        }
    }

}
