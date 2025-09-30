using Product.ApplicationCore.Contracts.Repositories;
using Product.ApplicationCore.Contracts.Services;
using Product.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure.Services
{
    public class VariationValueService : IVariationValueService
    {
        private readonly IVariationValueRepository _repo;
        public VariationValueService(IVariationValueRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<VariationValue> GetByVariationId(int variationId)
        {
            return _repo.GetByVariationId(variationId);
        }

        public VariationValue Save(int variationId, string value)
        {
            var entity = new VariationValue
            {
                VariationId = variationId,
                Value = value
            };
            return _repo.Insert(entity);
        }
    }
}
