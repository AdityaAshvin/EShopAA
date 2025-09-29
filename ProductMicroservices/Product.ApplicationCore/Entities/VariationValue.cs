using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.ApplicationCore.Entities
{
    public class VariationValue
    {
        public int Id { get; set; }

        public CategoryVariation Variation { get; set; }
        public int VariationId { get; set; }

        public string Value { get; set; }
        public ICollection<ProductVariationValue> ProductLinks { get; set; }
    }
}
