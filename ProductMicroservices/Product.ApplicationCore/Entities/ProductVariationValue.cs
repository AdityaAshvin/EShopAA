using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.ApplicationCore.Entities
{
    public class ProductVariationValue
    {
        public int ProductId { get; set; }
        public ProductEntity Product { get; set; } = default!;

        public VariationValue VariationValue { get; set; }
        public int VariationValueId { get; set; }
    }
}
