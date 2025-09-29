using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.ApplicationCore.Entities
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ProductCategory? Parent { get; set; }
        public int? ParentCategoryId { get; set; }

        public IEnumerable<ProductEntity> Products { get; set; }
        public IEnumerable<CategoryVariation> Variations { get; set; }
    }
}
