using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Order.ApplicationCore.Entities
{
    public class OrderDetails
    {
        public int Id { get; set; }
        [JsonIgnore]
        public OrderEntity? OrderEntity { get; set; }
        public int OrderId { get; set; }

        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
    }
}
