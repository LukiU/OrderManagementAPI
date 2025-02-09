using System.ComponentModel.DataAnnotations;

namespace OrderManagement.API.Models
{
    public class OrderItem
    {
        [Required]
        public Product Product { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; } = 1;
        [Required]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }

    }
}
