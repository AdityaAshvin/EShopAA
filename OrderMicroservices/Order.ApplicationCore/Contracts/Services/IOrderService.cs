using Order.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.ApplicationCore.Contracts.Services
{
    public interface IOrderService
    {
        OrderEntity CreateOrder(OrderEntity order);
        void UpdateOrder(OrderEntity order);
        void DeleteOrder(int id);
        IEnumerable<OrderEntity> GetAllOrders();
        OrderEntity? GetOrder(int id);
    }
}
