using Microsoft.AspNetCore.Mvc;
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
            var order = await _orderService.GetAllOrders();
            return Ok(order);

        }

        [HttpPost("{orderId}")]
        public async Task<IActionResult> AddOrder([FromBody] Order order)
        {
            var orderr = await _orderService.AddOrder(order);
            return Ok(orderr);
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
        public async Task<IActionResult> DeleteOrder([FromBody] DeleteRegionRequest orderRequest)
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
