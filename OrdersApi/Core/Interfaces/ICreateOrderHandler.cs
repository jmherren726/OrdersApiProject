using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICreateOrderHandler
    {
        public Task<string> Handle(Order order);
    }
}
