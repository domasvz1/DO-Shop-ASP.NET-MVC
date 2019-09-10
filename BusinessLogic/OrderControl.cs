using System;
using Mehdime.Entity;
using System.Collections.Generic;
// Used modules and interfaces in the project
using DataAccess.Interfaces;
using BusinessObjects.Orders;
using BusinessLogic.Interfaces;

namespace BusinessLogic
{
    public class OrderControl : IOrderControl
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IOrderRepository _orderRepository;

        public OrderControl(IDbContextScopeFactory dbContextScopeFactory, IOrderRepository OrderRepository)
        {
            _dbContextScopeFactory = dbContextScopeFactory ?? throw new ArgumentNullException("dbContextScopeFactory near orderControl broke");
            _orderRepository = OrderRepository ?? throw new ArgumentNullException("orderRepository near orderControl broke");
        }

        public ClientOrders GetClientOrder(int id)
        {
            using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
            {
                return _orderRepository.GetOrder(id);
            }
        }

        public IEnumerable<ClientOrders> GetAllOrders()
        {
            using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
            {
                return _orderRepository.GetAllOrders();
            }
        }

        public void UpdateOrderStatus(int id, OrderStatus status)
        {
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                var foundOrderObject = _orderRepository.GetOrder(id);
                if (foundOrderObject == null)
                {
                    throw new Exception("Order with such id was not found");
                }

                foundOrderObject.OrderStatus = status;

                _orderRepository.EditOrder(foundOrderObject);
                dbContextScope.SaveChanges();
            }
        }
    }
}
