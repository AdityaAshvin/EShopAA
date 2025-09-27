using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.ApplicationCore.Entities
{
    public class OrderEntity
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public int PaymentMethodId { get; set; }
        public string? PaymentName { get; set; }
        public string? ShippingAddress { get; set; }
        public string? ShippingMethod { get; set; }
        public decimal BillAmount { get; set; }
        public string? OrderStatus { get; set; }
        public IEnumerable<OrderDetails> OrderDetails { get; set; }
    }
}
