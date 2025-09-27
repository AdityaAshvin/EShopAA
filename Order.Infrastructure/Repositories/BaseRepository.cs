using Microsoft.EntityFrameworkCore;
using Order.ApplicationCore.Contracts.Repositories;
using Order.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.Repositories
{
    public class BaseRepository<T>: IRepository<T> where T : class
    {
        private readonly EShopDbContext _dbContext;
        public BaseRepository(EShopDbContext dbContext) {
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
