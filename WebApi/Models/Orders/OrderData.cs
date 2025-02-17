using System;
using System.Collections.Generic;
using BusinessEntities;

namespace WebApi.Models.Orders
{
    public class OrderData : IdObjectData
    {
        public OrderData(Order order, string userName, IList<OrderProductsData> orderProducts) : base(order)
        {
            OrderDate = order.OrderDate;
            Status = order.Status;
            UserName = userName;

            OrderProducts = orderProducts;
        }

        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public string UserName { get; set; }
        public IList<OrderProductsData> OrderProducts { get; set; }
    }
}