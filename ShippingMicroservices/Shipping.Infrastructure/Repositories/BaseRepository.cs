using Shipping.ApplicationCore.Contracts.Repositories;
using Shipping.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly ShippingDbContext _dbContext;
        public BaseRepository(ShippingDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public T DeleteById(int id)
        {
            var entity = _dbContext.Set<T>().Find(id);
            if (entity != null)
            {
                _dbContext.Set<T>().Remove(entity);
                _dbContext.SaveChanges();
                return entity;
            }
            return null;
        }

        public bool Exists(int id)
        {
            return _dbContext.Set<T>().Find(id) != null;
        }

        public IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            var orderId = _dbContext.Set<T>().Find(id);
            if (orderId == null)
            {
                return null;
            }
            return orderId;
        }

        public T Insert(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public T Update(T entity)
        {
            _dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
            return entity;
        }
    }
}
