using System.ComponentModel.DataAnnotations;

namespace OrderManagement.API.Models
{
    public class Customer
    {
        [Required]
        public string CustomerName { get; set; }
    }
}
