using Product.ApplicationCore.Contracts.Repositories;
using Product.ApplicationCore.Entities;
using Product.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<ProductEntity>, IProductRepository
    {
        private readonly ProductDbContext _dbContext;
        public ProductRepository(ProductDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<ProductEntity> GetByCategoryId(int categoryId)
        {
            return _dbContext.Products
                .AsNoTracking()
                .Where(p => p.CategoryId == categoryId)
                .ToList();
        }

        public IEnumerable<ProductEntity> GetByName(string name)
        {
            return _dbContext.Products
                .AsNoTracking()
                .Where(p => p.Name.Contains(name))
                .ToList();
        }
        public ProductEntity SetInactive(int id)
        {
            throw new NotImplementedException();
        }
    }
}
