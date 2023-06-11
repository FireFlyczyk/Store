using System.ComponentModel.DataAnnotations;

namespace OrderService.Dtos
{
    public class OrderCreateDto
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string CityDelivery { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string CustomerEmail { get; set; }
    }
}