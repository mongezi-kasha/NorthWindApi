using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.Models.Order;
using NorthWind.DAL;
using NorthWind.Services;

namespace Northwind.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly NorthWindContext _dbContext;
        private readonly IOrderService _orderService;
        public OrderController(NorthWindContext dbContext, IOrderService orderService)
        {
            _dbContext = dbContext;
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrders();
            return Ok(orders);
        }

        [HttpGet("single")]
        public async Task<IActionResult> GetOrder([FromQuery] string orderId)
        {
            var order = await _dbContext.Orders.Where(x => x.OrderId == int.Parse(orderId)).FirstOrDefaultAsync();
            return Ok(order);

        }

        [HttpPost("{orderId}")]
        public async Task<IActionResult> AddOrder([FromBody] Employee order)
        {
            await _dbContext.Employees.AddAsync(order);
            var result = await _dbContext.SaveChangesAsync();
            var isSuccessful = result > 0;

            var resultModel = new
            {
                Success = isSuccessful,
                Message = "Successfully added employee"
            };

            return Ok(resultModel);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderRequest orderRequest)
        {
            var order = await _orderService.GetOrderById(orderRequest.OrderID);
            if(order == null)
            {
                return NotFound();
            }

            order.ShipName = orderRequest.ShipName;

            await _orderService.UpdateOrder(order);

            return Ok(order);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteOrder([FromBody] DeleteOrderRequest orderRequest)
        {
            var order = await _orderService.GetOrderById(orderRequest.OrderID);
            if (order == null)
            {
                return NotFound();
            }

            await _orderService.DeleteOrder(order);

            return Ok(order);
        }
    }
}
