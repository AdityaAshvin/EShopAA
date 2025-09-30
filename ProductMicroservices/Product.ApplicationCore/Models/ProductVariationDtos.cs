using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.ApplicationCore.Models
{
    public class ProductVariationResponse
    {
        public int VariationValueId { get; set; }
        public string VariationName { get; set; }
        public string Value { get; set; }
    }
    public class ProductVariationSaveRequest
    {
        public int ProductId { get; set; }
        public List<int> VariationValueIds { get; set; }
    }

    public class VariationValueSaveRequest
    {
        public int VariationId { get; set; }
        public string Value { get; set; }
    }
}
