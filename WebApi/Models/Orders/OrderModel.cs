using BusinessEntities;
using System;
using System.Collections.Generic;

namespace WebApi.Models.Orders
{
    public class OrderModel
    {
        public OrderStatus Status { get; set; }
        public Guid UserId { get; set; } 
        public Dictionary<Guid, int> Products { get; set; }
    }
}