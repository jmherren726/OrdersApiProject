using Core.DTOs;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Handlers
{
    public class GetAllOrdersHandler : IGetAllOrdersHandler
    {
        private IOrdersRepository _ordersRepository;
        public GetAllOrdersHandler(IOrdersRepository ordersRepository) { 
            _ordersRepository = ordersRepository;
        }

        public async Task<List<Order>> Handle()
        {
            var ordersRequest = new List<Order>();

            try
            {
                ordersRequest = (List<Order>)await _ordersRepository.GetAllOrders();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return ordersRequest;
        }
    }
}
