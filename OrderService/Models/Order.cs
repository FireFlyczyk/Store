namespace OrderService.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string CityDelivery { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
    }
}