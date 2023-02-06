using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUpdateOrderHandler
    {
        public Task<Order> Handle(Order order);
    }
}
