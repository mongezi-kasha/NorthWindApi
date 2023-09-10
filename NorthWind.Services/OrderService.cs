using Microsoft.EntityFrameworkCore;
using NorthWind.DAL;
using NorthWind.Services.Models;

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
        private readonly NorthWindContext _dbContext;
        public OrderService(NorthWindContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            var orders = await _dbContext.Orders.ToListAsync();
            return orders;
        }

        public async Task UpdateOrder(Order order)
        {
            _dbContext.Orders.Update(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Order> GetOrderById(int id)
        {
            return await _dbContext.Orders.FirstOrDefaultAsync(x => x.OrderId == id);
        }

        public async Task DeleteOrder(Order order)
        {
            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();

        }

        public async Task<ServiceResponse<bool>> AddOrder(Order order)
        {
            _dbContext.Orders.Add(order);
            var result = await _dbContext.SaveChangesAsync();
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
