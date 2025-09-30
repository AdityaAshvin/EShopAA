using Promotion.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promotion.ApplicationCore.Contracts.Repositories
{
    public interface IPromotionRepository : IRepository<PromotionEntity>
    {
        IEnumerable<PromotionEntity> GetActive(DateTime asOf);
        IEnumerable<PromotionEntity> GetByProductName(string productName);
    }
}
