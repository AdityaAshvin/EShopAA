using Product.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product.ApplicationCore.Models;

namespace Product.ApplicationCore.Contracts.Repositories
{
    public interface IProductVariationRepository : IRepository<ProductVariationValue>
    {
        IEnumerable<ProductVariationResponse> GetByProductId(int productId);

        void SaveValues(int productId, IEnumerable<int> variationValueIds);
    }
}
