using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.ApplicationCore.Entities
{
    public class CategoryVariation
    {
        public int Id { get; set; }

        public ProductCategory Category { get; set; }
        public int CategoryId { get; set; }

        public string VariationName { get; set; }
        public ICollection<VariationValue> Values { get; set; }
    }
}
