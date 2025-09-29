using Order.ApplicationCore.Contracts.Repositories;
using Order.ApplicationCore.Contracts.Services;
using Order.ApplicationCore.Entities;
using Order.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orders;

        public OrderService(IOrderRepository orders)
        {
            _orders = orders;
        }
        public OrderEntity CreateOrder(OrderEntity order)
        {
            return _orders.Insert(order);
        }

        public void DeleteOrder(int id)
        {
            _orders.DeleteById(id);
        }

        public IEnumerable<OrderEntity> GetAllOrders()
        {
            return _orders.GetAll();
        }

        public OrderEntity? GetOrder(int id)
        {
            return _orders.GetById(id);
        }

        public IEnumerable<OrderEntity> GetOrdersByCustomer(int customerId)
        {
            return _orders.GetOrdersByCustomerId(customerId);
        }

        public IEnumerable<OrderEntity> GetPagedOrdersForAdmin(int pageIndex, int pageSize)
        {
            return _orders.GetPagedOrders(pageIndex, pageSize);
        }

        public void UpdateOrder(OrderEntity order)
        {
            _orders.Update(order);
        }
    }
}
