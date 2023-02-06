using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class Order
    {
        public Guid Id { get; set; }
        public string? OrderType { get; set; }
        public string? CustomerName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedByUsername { get; set; }
    }
}
