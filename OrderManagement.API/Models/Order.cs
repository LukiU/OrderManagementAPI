using MongoDB.Bson.Serialization.Attributes;
using OrderManagement.API.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace OrderManagement.API.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }

        [Required]
        public Customer Customer { get; set; } 

        [Required]
        public List<OrderItem> Items { get; set; }
       
        public OrderStatus OrderStatus { get; set; } = OrderStatus.New;
    }
}
