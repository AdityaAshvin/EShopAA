using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promotion.ApplicationCore.Entities
{
    public class PromotionDetails
    {
        public int Id { get; set; }

        public PromotionEntity PromotionEntity { get; set; }
        public int PromotionId { get; set; }

        public int ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }
    }
}
