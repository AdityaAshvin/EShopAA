using Product.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.ApplicationCore.Contracts.Services
{
    public interface IProductVariationService
    {
        IEnumerable<ProductVariationResponse> Get(int productId);
        void Save(int productId, IEnumerable<int> variationValueIds);
    }
}
