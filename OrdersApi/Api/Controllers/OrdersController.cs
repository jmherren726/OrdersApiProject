using Core.DTOs;
using Core.Enums;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private IGetAllOrdersHandler _getAllOrdersHandler;
        private IGetAllOrdersByUsernameHandler _getAllOrdersByUsernameHandler;
        private IGetAllOrdersByTypeHandler _getAllOrdersByTypeHandler;
        private ICreateOrderHandler _createOrderHandler;
        private IUpdateOrderHandler _updateOrderHandler;
        private IDeleteOrderHandler _deleteOrderHandler;
        public OrdersController(IGetAllOrdersHandler getAllOrdersHandler,
            IGetAllOrdersByUsernameHandler getAllOrdersByUsernameHandler,
            IGetAllOrdersByTypeHandler getAllOrdersByTypeHandler,
            ICreateOrderHandler createOrderHandler,
            IUpdateOrderHandler updateOrderHandler,
            IDeleteOrderHandler deleteOrderHandler)
        {
            _getAllOrdersHandler = getAllOrdersHandler;
            _getAllOrdersByUsernameHandler = getAllOrdersByUsernameHandler;
            _getAllOrdersByTypeHandler= getAllOrdersByTypeHandler;
            _createOrderHandler = createOrderHandler;
            _updateOrderHandler = updateOrderHandler;
            _deleteOrderHandler = deleteOrderHandler;
        }

        [HttpGet]
        [Route("GetAllOrders")]
        public async Task<IActionResult> GetAllOrders() {

            var response = await _getAllOrdersHandler.Handle();
            return Ok(response);
        }

        [HttpGet]
        [Route("GetAllOrdersByUsername")]
        public async Task<IActionResult> GetAllOrdersByUsername(string username)
        {

            var response = await _getAllOrdersByUsernameHandler.Handle(username);
            if (response != new List<Order>())
            {
                return Ok(response);
            }
            return Ok($"No orders found for user: {username}");
        }

        [HttpGet]
        [Route("GetAllOrdersByType")]
        public async Task<IActionResult> GetAllOrdersByType(string orderType)
        {
            if(!Enum.IsDefined(typeof(OrderType), orderType))
            {
                return BadRequest("Please ensure orderType is one of the following: {Standard, SaleOrder, PurchaseOrder, TransferOrder, ReturnOrder}");
            }

            var response = await _getAllOrdersByTypeHandler.Handle(orderType);
            if (response.Count() != 0)
            {
                return Ok(response);
            }
            return Ok("No orders found.");
        }

        [HttpPost]
        [Route("CreateOrder")]
        public async Task<IActionResult> CreateOrder(string orderType, string customerName)
        {
            Order newOrder = new Order();
            newOrder.OrderType = orderType;
            newOrder.CustomerName = customerName;
            newOrder.CreatedByUsername = Environment.UserName;
            string response = await _createOrderHandler.Handle(newOrder);
            if (string.IsNullOrEmpty(response))
            {
                return BadRequest("Please ensure parameters are not null, and orderType is one of the following: {Standard, SaleOrder, PurchaseOrder, TransferOrder, ReturnOrder}");
            }
            return Ok(response);
        }

        [HttpPatch]
        [Route("UpdateOrder")]
        public async Task<IActionResult> UpdateOrder(string id, string customerName, string orderType)
        {
            Order newOrder = new Order();
            newOrder.OrderType = orderType;
            newOrder.CustomerName = customerName;
            newOrder.Id = Guid.Parse(id);
            Order response = await _updateOrderHandler.Handle(newOrder);
            if (response.Id == new Guid())
            {
                return BadRequest("Please ensure parameters are not null, and orderType is one of the following: {Standard, SaleOrder, PurchaseOrder, TransferOrder, ReturnOrder}");
            }
            return Ok(response);
        }

        [HttpDelete]
        [Route("DeleteOrder")]
        public async Task<IActionResult> DeleteOrder(string id)
        {
            string response = await _deleteOrderHandler.Handle(id);
            if(string.IsNullOrEmpty(response))
            {
                return Ok("No order to delete.");
            }
            return Ok("Successfully deleted order.");
        }
    }
}
