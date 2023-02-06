using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IOrdersRepository
    {
        public Task<IEnumerable<Order>> GetAllOrders();
        public Task<IEnumerable<Order>> GetAllOrdersByUsername(string username);
        public Task<IEnumerable<Order>> GetAllOrdersByType(string orderType);
        public Task<string> CreateOrder(Order order);
        public Task<Order> UpdateOrder(Order order);
        public Task<string> DeleteOrder(string id);
    }
}
