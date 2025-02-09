using Microsoft.AspNetCore.Mvc;
using OrderManagement.API.Models;
using OrderManagement.API.Models.DTO;
using OrderManagement.API.Models.Enums;
using OrderManagement.API.Services;

namespace OrderManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        //POST: api/order
        [HttpPost]
        public async Task<IActionResult> CreateOrder(Order order)
        {
            var newOrder = await _orderRepository.CreateOrder(order);
            return CreatedAtAction(nameof(CreateOrder), newOrder);
        }

        //GET: api/order
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _orderRepository.GetOrders();
            return Ok(orders);
        }

        //GET: api/order/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(string id)
        {
            var order = await _orderRepository.GetOrderById(id);
            if (Equals(order, null))
                return NotFound($"Order with ID: {id} was not found");
            return Ok(order);
        }

        //PATCH: api/order/{id}/status
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateOrderStatus(string id, [FromBody] OrderStatusUpdateDTO updateDTO)
        {
            var order = await _orderRepository.GetOrderById(id);
            if (Equals(order, null))
                return NotFound($"Order with ID: {id} was not found");

            var currentStatus = order.OrderStatus;

            if((Equals(updateDTO.Status, OrderStatus.InProgress) && Equals(currentStatus, OrderStatus.New)) ||
               (Equals(updateDTO.Status, OrderStatus.Completed) && Equals(currentStatus, OrderStatus.InProgress)))
            {
                await _orderRepository.UpdateOrderStatus(id, updateDTO.Status);
                return NoContent();
            }

            return Conflict("Invalid update status sequence");
        }

    }
}
