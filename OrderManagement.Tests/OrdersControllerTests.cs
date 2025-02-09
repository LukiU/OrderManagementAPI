using Microsoft.AspNetCore.Mvc;
using Moq;
using OrderManagement.API.Controllers;
using OrderManagement.API.Models;
using OrderManagement.API.Models.DTO;
using OrderManagement.API.Models.Enums;
using OrderManagement.API.Services;

namespace OrderManagement.Tests
{
    public class OrdersControllerTests
    {
        private readonly Mock<IOrderRepository> _mockOrderRepository;
        private readonly OrderController _controller;

        public OrdersControllerTests()
        {
            _mockOrderRepository = new Mock<IOrderRepository>();
            _controller = new OrderController( _mockOrderRepository.Object );
        }

        [Fact]
        public async Task GetOrders_ReturnsOkResult_WithOrders()
        {
            var orders = new List<Order> { GetTestOrder(out string id) };
            _mockOrderRepository.Setup(repo => repo.GetOrders()).ReturnsAsync(orders);

            var result = await _controller.GetOrders();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedOrders = Assert.IsType<List<Order>>(okResult.Value);
            Assert.Single(returnedOrders);
            Assert.Equal(id, returnedOrders.First().Id);
        }

        [Fact]
        public async Task GetOrderById_ReturnsOkResult_WithOrder()
        {
            var order = GetTestOrder(out string id);
            _mockOrderRepository.Setup(repo => repo.GetOrderById(id)).ReturnsAsync(order);

            var result = await _controller.GetOrderById(id);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedOrder = Assert.IsType<Order>(okResult.Value);
            Assert.Equal(id, returnedOrder.Id);
        }

        [Fact]
        public async Task GetOrderById_ReturnsNotFound_WhenOrderNotFound()
        {
            _mockOrderRepository.Setup(repo => repo.GetOrderById("-1")).ReturnsAsync((Order)null);

            var result = await _controller.GetOrderById("-1");

            Assert.IsType<NotFoundObjectResult>(result);
        }


        [Fact]
        public async Task CreateOrder_ReturnsCreatedAtAction_WithCreatedOrder()
        {
            var order = GetTestOrder(out string id); 
            var createdOrder = GetTestOrder(out string createAtID, id);
            _mockOrderRepository.Setup(repo => repo.CreateOrder(order)).ReturnsAsync(createdOrder);

            var result = await _controller.CreateOrder(order);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnedOrder = Assert.IsType<Order>(createdAtActionResult.Value);
            Assert.Equal(id, returnedOrder.Id);
        }

        [Fact]
        public async Task UpdateOrderStatus_ReturnsNoContent_WhenStatusUpdated()
        {
            var order = GetTestOrder(out string id);
            _mockOrderRepository.Setup(repo => repo.GetOrderById(id)).ReturnsAsync(order);

            var result = await _controller.UpdateOrderStatus(id, new OrderStatusUpdateDTO { Status = OrderStatus.InProgress });

            Assert.IsType<NoContentResult>(result);
            _mockOrderRepository.Verify(repo => repo.UpdateOrderStatus(id, OrderStatus.InProgress), Times.Once);
        }

        [Fact]
        public async Task UpdateOrderStatus_ReturnsBadRequest_WhenInvalidStatusUpdate()
        {
            var order = GetTestOrder(out string id);
            _mockOrderRepository.Setup(repo => repo.GetOrderById(id)).ReturnsAsync(order);

            var result = await _controller.UpdateOrderStatus(id, new OrderStatusUpdateDTO { Status = OrderStatus.New });

            Assert.IsType<ConflictObjectResult>(result);
        }

        [Fact]
        public async Task UpdateOrderStatus_ReturnsNotFound_WhenOrderNotFound()
        {
            _mockOrderRepository.Setup(repo => repo.GetOrderById("-1")).ReturnsAsync((Order)null);

            var result = await _controller.UpdateOrderStatus("-1", new OrderStatusUpdateDTO { Status = OrderStatus.InProgress });

            Assert.IsType<NotFoundObjectResult>(result);
        }



        private Order GetTestOrder(out string id, string presetID = "")
        {
            id = presetID; 
            if(string.IsNullOrEmpty(presetID))
                id = Guid.NewGuid().ToString();
            return
            new Order()
            {
                Id = id,
                Customer = new Customer()
                {
                    CustomerName = "Test Customer"
                },
                Items = new List<OrderItem>() {
                    new OrderItem
                    {
                        Product = new Product()
                        {
                            ProductName = "Test Product"
                        }, 
                        Price = 19.99m,
                        Quantity = 1
                    }
                }
            };
        }
    }
}