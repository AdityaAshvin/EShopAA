using Order.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.ApplicationCore.Contracts.Repositories
{
    public interface IOrderRepository : IRepository<OrderEntity>
    {
        IEnumerable<OrderEntity> GetOrdersWithDetails();
        IEnumerable<OrderEntity> GetOrdersByCustomerId(int customerId);
        IEnumerable<OrderEntity> GetPagedOrders(int pageIndex, int pageSize);
    }
}
