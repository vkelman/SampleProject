using System.Net.Http;
using System;
using System.Web.Http;
using Core.Services.Orders;
using WebApi.Models.Orders;
using BusinessEntities;
using WebApi.Models.Products;

namespace WebApi.Controllers
{
    [RoutePrefix("orders")]
    public class OrderController : BaseApiController
    {
        private readonly ICreateOrderService _createOrderService;
        private readonly IDeleteOrderService _deleteOrderService;
        private readonly IGetOrderService _getOrderService;
        private readonly IUpdateOrderService _updateOrderService;

        public OrderController(
            ICreateOrderService createOrderService,
            IDeleteOrderService deleteOrderService,
            IGetOrderService getOrderService,
            IUpdateOrderService updateOrderService)
        {
            _createOrderService = createOrderService;
            _deleteOrderService = deleteOrderService;
            _getOrderService = getOrderService;
            _updateOrderService = updateOrderService;
        }

        [Route("{orderId:guid}/create")]
        [HttpPost]
        public HttpResponseMessage CreateOrder(Guid orderId, [FromBody] OrderModel model)
        {
            var existingOrder = _getOrderService.GetOrder(orderId);
            if (existingOrder != null)
            {
                return AlreadyExist("Order");
            }


            var product = _createOrderService.Create(orderId, model.Name, model.Description, model.Type, model.Price);

            return Found(new OrderData(order));
        }
    }
}
