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

            foreach (var productId in model.Products.Keys)
            {
                var product = _getProductService.GetProduct(productId);
                if (product == null)
                {
                    return DoesNotExist("Product");
                }
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

            foreach (var productId in model.Products.Keys)
            {
                var product = _getProductService.GetProduct(productId);
                if (product == null)
                {
                    return DoesNotExist("Product");
                }
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
 
        private IList<OrderProductsData> GetOrderProducts(Order order)
        {
            var orderProducts = new List<OrderProductsData>();

            foreach (var productAndQuantity in order.Products)
            {
                var product = _getProductService.GetProduct(productAndQuantity.Key);
                var quantity = productAndQuantity.Value;
                var orderProductsData = new OrderProductsData(product.Name, quantity);
                orderProducts.Add(orderProductsData);
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
