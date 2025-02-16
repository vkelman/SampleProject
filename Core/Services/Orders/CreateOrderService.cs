using BusinessEntities;
using System;
using Core.Factories;
using Data.Repositories;
using System.Collections.Generic;
using Common;

namespace Core.Services.Orders
{
    [AutoRegister]
    public class CreateOrderService : ICreateOrderService
    {
        private readonly IIdObjectFactory<Order> _orderFactory;
        private readonly IUpdateOrderService _updateOrderService;
        private readonly IOrderRepository _orderRepository;

        public CreateOrderService(IIdObjectFactory<Order> orderFactory, IOrderRepository orderRepository, IUpdateOrderService updateOrderService)
        {
            _orderFactory = orderFactory;
            _orderRepository = orderRepository;
            _updateOrderService = updateOrderService;
        }

        public Order Create(Guid id, Guid userId, Dictionary<Guid, int> products)
        {
            var order = _orderFactory.Create(id);

            order.UserId = userId;
            _updateOrderService.Update(order, DateTime.Now, OrderStatus.New, products);
            _orderRepository.Save(order);

            return order;
        }
    }
}