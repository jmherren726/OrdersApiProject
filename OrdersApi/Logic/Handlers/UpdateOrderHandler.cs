using Core.DTOs;
using Core.Enums;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Handlers
{
    public class UpdateOrderHandler : IUpdateOrderHandler
    {
        private IOrdersRepository _ordersRepository;

        public UpdateOrderHandler(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<Order> Handle(Order order)
        {
            Order result = new Order();
            if (validateOrder(order))
            {
                result = await _ordersRepository.UpdateOrder(order);
            }
            return result;
        }

        public bool validateOrder(Order order)
        {
            if (string.IsNullOrEmpty(order.OrderType) || string.IsNullOrEmpty(order.CustomerName) || 
                !(Enum.IsDefined(typeof(OrderType), order.OrderType)))
            {
                return false;
            }
            return true;
        }
    }
}
