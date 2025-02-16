using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Core.Services.Orders
{
    public interface IGetOrderService
    {
        Order GetOrder(Guid id);

        IEnumerable<Order> GetOrders(DateTime? orderDate = null, OrderStatus? orderStatus = null, Guid? userId = null);
    }
}