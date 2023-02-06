using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Handlers
{
    public class DeleteOrderHandler : IDeleteOrderHandler
    {
        private IOrdersRepository _ordersRepository;
        public DeleteOrderHandler(IOrdersRepository ordersRepository) 
        {
            _ordersRepository = ordersRepository;
        }

        public async Task<string> Handle(string id)
        {
            string result = "";
            try
            {
                result = await _ordersRepository.DeleteOrder(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
    }
}
