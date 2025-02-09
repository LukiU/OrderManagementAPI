using OrderManagement.API.Models;
using OrderManagement.API.Models.Enums;

namespace OrderManagement.API.Services
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrder(Order order);
        Task<List<Order>> GetOrders();
        Task<Order> GetOrderById(string id);
        Task UpdateOrderStatus(string id, OrderStatus orderStatus);
    }
}
