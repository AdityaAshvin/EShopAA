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
    public class VariationValueRepository : BaseRepository<VariationValue>, IVariationValueRepository
    {
        private readonly ProductDbContext _dbContext;
        public VariationValueRepository(ProductDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<VariationValue> GetByVariationId(int variationId)
        {
            return _dbContext.VariationValues
                .AsNoTracking()
                .Where(vv => vv.VariationId == variationId)
                .ToList();
        }
    }
}
