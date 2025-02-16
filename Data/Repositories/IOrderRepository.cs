using BusinessEntities;
using System;
using System.Collections.Generic;

namespace Data.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        IEnumerable<Order> Get(DateTime? orderDate = null, Guid? userId = null);
        void DeleteAll();
    }
}