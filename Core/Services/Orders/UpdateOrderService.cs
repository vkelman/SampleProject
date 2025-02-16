using BusinessEntities;
using JetBrains.Annotations;
using System.Collections.Generic;
using System;

namespace Core.Services.Orders
{
    public class UpdateOrderService : IUpdateOrderService
    {
        public Order Update(Order order, DateTime? updateDateTime, OrderStatus? status, [CanBeNull] Dictionary<Guid, int> products, bool ignoreNullValues = false)
        {
            if (order.Status != OrderStatus.Undefined) //// When order is created by IIdObjectFactory<Order>:: Create(), its status is always Undefined; there is no need to add history.
            {
                order.AddToOrderHistory(order.OrderHistory, order.Products);
            }

            if (!(ignoreNullValues && !updateDateTime.HasValue)) order.SetOrderDate(updateDateTime);
            // ReSharper disable once PossibleInvalidOperationException
            if (!(ignoreNullValues && !status.HasValue)) order.SetStatus(status.Value);
            if (!(ignoreNullValues && products == null)) order.SetProducts(products);

            return order;
        }
    }
}