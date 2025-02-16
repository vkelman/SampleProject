using System;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities;

namespace Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private static readonly IList<Order> Orders = new List<Order>();

        public void Save(Order order)
        {
            var existingOrder = Get(order.Id);

            if (existingOrder != null)
            {
                Orders.Remove(existingOrder);
            }

            Orders.Add(order);
        }

        public void Delete(Order order)
        {
            Orders.Remove(order);
        }

        public Order Get(Guid id)
        {
            return Orders.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Order> Get(DateTime? orderDate = null, OrderStatus? orderStatus = null, Guid? userId = null)
        {
            var result = Orders;

            if(orderDate != null)
            {
                result = result.Where(x => x.OrderDate.Date == orderDate.Value.Date).ToList(); //// Note: Comparing Date part only
            }

            if (orderStatus != null)
            {
                result = result.Where(x => x.Status == orderStatus.Value).ToList();
            }

            if (userId != null)
            {
                result = result.Where(x => x.UserId == userId.Value).ToList();
            }

            return result;
        }

        public void DeleteAll()
        {
            Orders.Clear();
        }
    }
}