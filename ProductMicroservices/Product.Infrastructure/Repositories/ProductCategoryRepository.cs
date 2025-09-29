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
    public class ProductCategoryRepository : BaseRepository<ProductCategory>, IProductCategoryRepository
    {
        private readonly ProductDbContext _dbContext;
        public ProductCategoryRepository(ProductDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<ProductCategory> GetByParentCategoryId(int? parentCategoryId)
        {
            var isRoot = parentCategoryId is null || parentCategoryId == 0;

            return _dbContext.ProductCategories
                      .AsNoTracking()
                      .Where(c => isRoot ? c.ParentCategoryId == null
                                         : c.ParentCategoryId == parentCategoryId)
                      .OrderBy(c => c.Name)
                      .ToList();
        }
    }
}
