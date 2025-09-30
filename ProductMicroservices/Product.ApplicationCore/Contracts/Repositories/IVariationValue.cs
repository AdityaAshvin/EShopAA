using Product.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.ApplicationCore.Contracts.Repositories
{
    public interface IVariationValueRepository : IRepository<VariationValue>
    {
        IEnumerable<VariationValue> GetByVariationId(int variationId);
    }
}
