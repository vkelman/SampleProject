using System;
using System.Collections.Generic;
using BusinessEntities;
using JetBrains.Annotations;

namespace Core.Services.Orders
{
    public interface IUpdateOrderService
    {
        /// <summary>
        /// Updates the order properties with the given values.
        /// </summary>
        /// <param name="order">Order object</param>
        /// <param name="updateDateTime">DateTime of order update</param>
        /// <param name="status">new order status</param>
        /// <param name="products">order products</param>
        /// <param name="ignoreNullValues">If true, don't replace order properties with nulls (suitable for update operation with optional parameters;
        /// if false, raise a corresponding exception if some of the parameters are nulls (suitable for create operation).</param>
        /// <returns>updated Order object</returns>
        Order Update(Order order, DateTime? updateDateTime, OrderStatus? status, [CanBeNull] Dictionary<Guid, int> products, bool ignoreNullValues = false);
    }
}