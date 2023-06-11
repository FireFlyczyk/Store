using OrderService.Models;
using System;
using System.Collections.Generic;

namespace OrderService.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public void CreateOrder(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            try
            {
                _context.Orders.Add(order);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create order.", ex);
            }
        }

        public IEnumerable<Order> GetAllOrders()
        {
            try
            {
                return _context.Orders.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get all orders.", ex);
            }
        }

        public Order GetOrderById(int id)
        {
            try
            {
                return _context.Orders.FirstOrDefault(o => o.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get order by id.", ex);
            }
        }

        public bool SaveChanges()
        {
            try
            {
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to save changes.", ex);
            }
        }
    }
}
