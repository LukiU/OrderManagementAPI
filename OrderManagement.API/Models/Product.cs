using System.ComponentModel.DataAnnotations;

namespace OrderManagement.API.Models
{
    public class Product
    {
        [Required]
        public string ProductName { get; set; }
    }
}
