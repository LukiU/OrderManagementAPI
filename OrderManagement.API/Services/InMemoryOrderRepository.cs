using OrderManagement.API.Models;
using OrderManagement.API.Models.Enums;

namespace OrderManagement.API.Services
{
    public class InMemoryOrderRepository : IOrderRepository
    {
        private readonly List<Order> _orders = new List<Order>();

        public Task<Order> CreateOrder(Order order)
        {
            order.Id = Guid.NewGuid().ToString();
            _orders.Add(order);
            return Task.FromResult(order);
        }

        public Task<Order> GetOrderById(string id)
        {
            return Task.FromResult(_orders.FirstOrDefault(o => Equals(o.Id, id)));
        }

        public Task<List<Order>> GetOrders()
        {
            return Task.FromResult(_orders.ToList());
        }

        public Task UpdateOrderStatus(string id, OrderStatus orderStatus)
        {
            var order = _orders.FirstOrDefault(o => Equals(o.Id, id));
            if (Equals(order, null))
                return Task.FromException(new Exception("Order not found"));

            order.OrderStatus = orderStatus;
            return Task.CompletedTask;
        }
    }
}
