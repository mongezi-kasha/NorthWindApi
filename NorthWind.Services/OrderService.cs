using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Northwind.Lib.CommonData;
using NorthWind.DAL;
using NorthWind.Services.Models;
using Specs = Northwind.Lib.CommonData.Specifications;

namespace NorthWind.Services
{
    public interface IOrderService
    {
        Task<ServiceResponse<List<Order>>> GetAllOrders();
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

        public async Task<ServiceResponse<List<Order>>> GetAllOrders()
        {
            var orders = await _dbContext.Orders.ToListAsync();
            return new ServiceResponse<List<Order>> { Data = orders, IsSuccessful = true, Message = "Success" };
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

        public async Task<Order> GetOrderByCustomerId(string customerId)
        {
            return await _dbContext.Orders.FirstOrDefaultAsync(x => x.CustomerId == customerId);
        }

        public async Task DeleteOrder(Order order)
        {
            _dbContext.Orders.Update(order);
            await _dbContext.SaveChangesAsync();

        }

        public async Task<ServiceResponse<bool>> AddOrder(Order order)
        {
            _dbContext.Orders.AddAsync(order);
            var result = await _dbContext.SaveChangesAsync();
            var isSuccessful = result > 0;

            var resultModel = new ServiceResponse<bool>
            {
                IsSuccessful = isSuccessful,
                Message = "Successfully added Order"
            };

            return resultModel;
        }
    }

}
