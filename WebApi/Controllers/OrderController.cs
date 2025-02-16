using System.Net.Http;
using System;
using System.Web.Http;
using Core.Services.Orders;
using WebApi.Models.Orders;
using Core.Services.Users;
using System.Collections.Generic;
using System.Linq;
using Core.Services.Products;
using BusinessEntities;

namespace WebApi.Controllers
{
    [RoutePrefix("orders")]
    public class OrderController : BaseApiController
    {
        private readonly ICreateOrderService _createOrderService;
        private readonly IDeleteOrderService _deleteOrderService;
        private readonly IGetOrderService _getOrderService;
        private readonly IUpdateOrderService _updateOrderService;
        private readonly IGetUserService _userService;
        private readonly IGetProductService _getProductService;

        public OrderController(
            ICreateOrderService createOrderService,
            IDeleteOrderService deleteOrderService,
            IGetOrderService getOrderService,
            IUpdateOrderService updateOrderService,
            IGetUserService userService,
            IGetProductService getProductService)
        {
            _createOrderService = createOrderService;
            _deleteOrderService = deleteOrderService;
            _getOrderService = getOrderService;
            _updateOrderService = updateOrderService;
            _userService = userService;
            _getProductService = getProductService;
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

            var user = _userService.GetUser(model.UserId);
            if (user == null)
            {
                return DoesNotExist("User");
            }

            var order = _createOrderService.Create(orderId, model.UserId, model.Products);

            var orderProducts = GetOrderProducts(order);

            return Found(new OrderData(order, user.Name, orderProducts));
        }

        [Route("{orderId:guid}/update")]
        [HttpPost]
        public HttpResponseMessage UpdateOrder(Guid orderId, [FromBody] OrderModel model)
        {
            var order = _getOrderService.GetOrder(orderId);
            if (order == null)
            {
                return DoesNotExist("Order");
            }

            var user = _userService.GetUser(model.UserId);
            if (user == null)
            {
                return DoesNotExist("User");
            }

            order = _updateOrderService.Update(order, DateTime.Now, model.Status, model.Products);

            var orderProducts = GetOrderProducts(order);

            return Found(new OrderData(order, user.Name, orderProducts));
        }

        [Route("{orderId:guid}/delete")]
        [HttpDelete]
        public HttpResponseMessage DeleteOrder(Guid orderId)
        {
            var order = _getOrderService.GetOrder(orderId);
            if (order == null)
            {
                return DoesNotExist("Order");
            }

            _deleteOrderService.Delete(order);

            return Found();
        }
  
        [Route("{orderId:guid}")]
        [HttpGet]
        public HttpResponseMessage GetOrder(Guid orderId)
        {
            var order = _getOrderService.GetOrder(orderId);
                 
            if (order == null)
            {
                return DoesNotExist("Order");
            }

            string userName = GetUserName(_userService.GetUser(order.UserId));

            var orderProducts = GetOrderProducts(order);

            return Found(new OrderData(order, userName, orderProducts));
        }

        //// Note: Specifying take < 0 would return all orders. It may be suitable or not depending on the requirements and database size.
        //// Note: Filtering by date uses Date part only, ignoring time. See OrderRepository::Get(). 
        [Route("list")]
        [HttpGet]
        public HttpResponseMessage GetOrders(int skip, int take, DateTime? date = null, OrderStatus? status = null, Guid? userId = null)
        {
            var orders = take < 0
                ? _getOrderService.GetOrders(date, status, userId)
                    .Skip(skip)
                    .Select(q => new OrderData(q, GetUserName(_userService.GetUser(q.UserId)), GetOrderProducts(q)))
                    .ToList()
                : _getOrderService.GetOrders(date, status, userId)
                    .Skip(skip).Take(take)
                    .Select(q => new OrderData(q, GetUserName(_userService.GetUser(q.UserId)), GetOrderProducts(q)))
                    .ToList();

            return Found(orders);
        }

        [Route("clear")]
        [HttpDelete]
        public HttpResponseMessage DeleteAllOrders()
        {
            _deleteOrderService.DeleteAll();

            return Found();
        }
 
        private IList<(string productName, int quantity)> GetOrderProducts(Order order)
        {
            var orderProducts = new List<(string productName, int quantity)>();

            foreach (var orderProduct in order.Products)
            {
                var productId = orderProduct.Key;
                var productQuantity = orderProduct.Value;
                var product = _getProductService.GetProduct(productId);
                orderProducts.Add((product.Name, productQuantity));
            }

            return orderProducts;
        }

        private string GetUserName(User user)
        {
            string userName = user == null ? "Unknown" : user.Name;  //// Note: User might have been deleted.

            return userName;
        }
    }
}
