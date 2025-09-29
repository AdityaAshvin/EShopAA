using Microsoft.EntityFrameworkCore;
using Order.ApplicationCore.Contracts.Repositories;
using Order.ApplicationCore.Entities;
using Order.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EShopDbContext _dbContext;

        public OrderRepository(EShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public OrderEntity DeleteById(int id)
        {
            var entity = _dbContext.Orders.Find(id);
            if (entity == null) return null;
            _dbContext.Orders.Remove(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public IEnumerable<OrderEntity> GetAll()
        {
            return _dbContext.Orders.ToList();
        }

        public OrderEntity GetById(int id)
        {
            var entity = _dbContext.Orders.Find(id);
            if(entity == null)
                    return null;
            return entity;
        }

        public IEnumerable<OrderEntity> GetOrdersByCustomerId(int customerId)
        {
            return _dbContext.Orders.Include(o => o.OrderDetails).Where(o => o.CustomerId == customerId)
                .OrderByDescending(o => o.OrderDate).ToList();
        }

        public IEnumerable<OrderEntity> GetOrdersWithDetails()
        {
            return _dbContext.Orders.Include(o => o.OrderDetails).ToList();
        }

        public IEnumerable<OrderEntity> GetPagedOrders(int pageIndex, int pageSize)
        {
            return _dbContext.Orders.Include(o => o.OrderDetails).OrderByDescending(o => o.OrderDate)
                     .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public OrderEntity Insert(OrderEntity entity)
        {
            _dbContext.Orders.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public OrderEntity Update(OrderEntity entity)
        {
            _dbContext.Orders.Update(entity);
            _dbContext.SaveChanges();
            return entity;
        }
    }
}
