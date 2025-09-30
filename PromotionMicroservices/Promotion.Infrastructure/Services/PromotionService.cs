using Promotion.ApplicationCore.Contracts.Repositories;
using Promotion.ApplicationCore.Contracts.Services;
using Promotion.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promotion.Infrastructure.Services
{
    public class PromotionService : IPromotionService
    {
        private readonly IPromotionRepository _repo;
        public PromotionService(IPromotionRepository repo) => _repo = repo;

        public IEnumerable<PromotionEntity> GetList() => _repo.GetAll();
        public PromotionEntity GetById(int id) => _repo.GetById(id);
        public PromotionEntity Save(PromotionEntity promotion) => _repo.Insert(promotion);
        public PromotionEntity Update(PromotionEntity promotion) => _repo.Update(promotion);
        public PromotionEntity Delete(int id) => _repo.DeleteById(id);

        public IEnumerable<PromotionEntity> GetActive(DateTime? asOf = null) =>
            _repo.GetActive(asOf ?? DateTime.UtcNow);

        public IEnumerable<PromotionEntity> GetByProductName(string productName) =>
            _repo.GetByProductName(productName);

        public bool Exists(int id) => _repo.Exists(id);
    }
}