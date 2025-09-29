using Product.ApplicationCore.Contracts.Repositories;
using Product.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly ProductDbContext _dbContext;
        public BaseRepository(ProductDbContext dbContext)
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
