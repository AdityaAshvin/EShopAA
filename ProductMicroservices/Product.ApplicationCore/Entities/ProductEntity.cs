using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.ApplicationCore.Entities
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ProductCategory Category { get; set; }
        public int CategoryId { get; set; }

        public decimal Price { get; set; }
        public int Qty { get; set; }
        public string ProductImage { get; set; }
        public string SKU { get; set; }

        public IEnumerable<ProductVariationValue> VariationValues { get; set; }
    }
}
