using Product.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.ApplicationCore.Contracts.Services
{
    public interface IVariationValueService
    {
        IEnumerable<VariationValue> GetByVariationId(int variationId);
        VariationValue Save(int variationId, string value);
    }
}
