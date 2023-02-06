using Core.DTOs;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Handlers
{
    public class GetAllOrdersByTypeHandler : IGetAllOrdersByTypeHandler
    {
        private IOrdersRepository _ordersRepository;

        public GetAllOrdersByTypeHandler(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<List<Order>> Handle(string orderType)
        {
            var ordersRequest = new List<Order>();

            try
            {
                ordersRequest = (List<Order>)await _ordersRepository.GetAllOrdersByType(orderType);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return ordersRequest;
        }
    }
}
