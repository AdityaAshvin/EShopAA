using Product.ApplicationCore.Contracts.Repositories;
using Product.ApplicationCore.Entities;
using Product.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product.ApplicationCore.Models;

namespace Product.Infrastructure.Repositories
{
    public class ProductVariationRepository : BaseRepository<ProductVariationValue>, IProductVariationRepository
    {
        private readonly ProductDbContext _dbContext;
        public ProductVariationRepository(ProductDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<ProductVariationResponse> GetByProductId(int productId)
        {
            var query = _dbContext.ProductVariationValues
                .AsNoTracking()
                .Where(pv => pv.ProductId == productId)
                .Join(_dbContext.VariationValues.AsNoTracking(),
                      pv => pv.VariationValueId,
                      vv => vv.Id,
                      (pv, vv) => new { vv })
                .Join(_dbContext.CategoryVariations.AsNoTracking(),
                      x => x.vv.VariationId,
                      cv => cv.Id,               
                      (x, cv) => new ProductVariationResponse
                      {
                          VariationValueId = x.vv.Id,
                          VariationName = cv.VariationName,
                          Value = x.vv.Value
                      });

            if(query is null)
            {
                return null;
            }
            return query.ToList();
        }

        public void SaveValues(int productId, IEnumerable<int> variationValueIds)
        {
            var existing = _dbContext.ProductVariationValues
                .Where(pv => pv.ProductId == productId)
                .Select(pv => pv.VariationValueId)
                .ToHashSet();

            var toAdd = variationValueIds
                .Where(id => !existing.Contains(id))
                .Select(id => new ProductVariationValue
                {
                    ProductId = productId,
                    VariationValueId = id
                })
                .ToList();

            if (toAdd.Count > 0)
            {
                _dbContext.ProductVariationValues.AddRange(toAdd);
                _dbContext.SaveChanges();
            }
        }
    }
}
