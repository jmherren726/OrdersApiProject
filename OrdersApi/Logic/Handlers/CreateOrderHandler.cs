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
    public class CreateOrderHandler : ICreateOrderHandler
    {
        public IOrdersRepository _ordersRepository;
        public CreateOrderHandler(IOrdersRepository ordersRepository) 
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<string> Handle(Order order)
        {
            string result = "";
            if(validateOrder(order))
            {
                result = await _ordersRepository.CreateOrder(order);
            }
            return result;
        }

        public bool validateOrder(Order order)
        {
            if (string.IsNullOrEmpty(order.OrderType) || string.IsNullOrEmpty(order.CustomerName)
                || string.IsNullOrEmpty(order.CreatedByUsername) || !(Enum.IsDefined(typeof(OrderType), order.OrderType)))
            {
                return false;
            }
            return true;
        }
    }
}
