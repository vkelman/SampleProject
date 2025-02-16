using BusinessEntities;
using System;
using Core.Factories;
using Data.Repositories;

namespace Core.Services.Orders
{
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

        public Order Create(Guid id, DateTime orderDateTime, Guid userId)
        {
            var order = _orderFactory.Create(id);
            _updateOrderService.Update(order, name, description, type, price);
            _orderRepository.Save(order);

            return order;
        }
    }
}