using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGetAllOrdersByTypeHandler
    {
        public Task<List<Order>> Handle(string orderType);
    }
}
