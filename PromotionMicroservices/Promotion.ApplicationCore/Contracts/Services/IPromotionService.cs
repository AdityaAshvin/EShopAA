using Promotion.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promotion.ApplicationCore.Contracts.Services
{
    public interface IPromotionService
    {
        IEnumerable<PromotionEntity> GetList();
        PromotionEntity GetById(int id);
        PromotionEntity Save(PromotionEntity promotion);
        PromotionEntity Update(PromotionEntity promotion);
        PromotionEntity Delete(int id);

        IEnumerable<PromotionEntity> GetActive(DateTime? asOf = null);
        IEnumerable<PromotionEntity> GetByProductName(string productName);
        bool Exists(int id);
    }
}
