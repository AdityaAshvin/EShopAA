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
    public class CategoryVariationRepository : BaseRepository<CategoryVariation>, ICategoryVariationRepository
    {
        private readonly ProductDbContext _dbContext;
        public CategoryVariationRepository(ProductDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<CategoryVariation> GetByCategoryId(int categoryId)
        {
            return _dbContext.CategoryVariations
               .AsNoTracking()
               .Where(v => v.CategoryId == categoryId)
               .OrderBy(v => v.VariationName)
               .ToList();
        }
    }
}
