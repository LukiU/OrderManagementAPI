using MongoDB.Driver;
using OrderManagement.API.Models;
using OrderManagement.API.Models.Enums;
using System.Numerics;

namespace OrderManagement.API.Services
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoCollection<Order> _orders;
        public OrderRepository(IMongoClient client, string databaseName, string collectionName) {

            _orders = client.GetDatabase(databaseName).GetCollection<Order>(collectionName);
        }

        public async Task<Order> CreateOrder(Order order)
        {
            await _orders.InsertOneAsync(order);
            return order;
        }

        public async Task<Order> GetOrderById(string id)
        {
            return await _orders.Find(order => order.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Order>> GetOrders()
        {
            return await _orders.Find(_ => true).ToListAsync();
        }

        public async Task UpdateOrderStatus(string id, OrderStatus orderStatus)
        {
            var update = Builders<Order>.Update.Set(order => order.OrderStatus, orderStatus);
            await _orders.UpdateOneAsync(order => order.Id == id, update);
        }
    }
}
