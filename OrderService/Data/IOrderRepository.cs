using OrderService.Models;

namespace OrderService.Data
{
    public interface IOrderRepository
    {
        bool SaveChanges();
        IEnumerable<Order> GetAllOrders();
        Order GetOrderById(int id);
        void CreateOrder(Order order);
    }
}