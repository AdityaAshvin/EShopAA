using Product.ApplicationCore.Contracts.Repositories;
using Product.ApplicationCore.Contracts.Services;
using Product.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure.Services
{
    public class ProductVariationService : IProductVariationService
    {
        private readonly IProductVariationRepository _repo;
        public ProductVariationService(IProductVariationRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<ProductVariationResponse> Get(int productId)
        {
            return _repo.GetByProductId(productId);
        }
        public void Save(int productId, IEnumerable<int> variationValueIds)
        {
            _repo.SaveValues(productId, variationValueIds);
        }
    }
}
