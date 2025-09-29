using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.ApplicationCore.Entities
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        public PaymentType PaymentType { get; set; }
        public int PaymentTypeId { get; set; }
        public string Provider {  get; set; }
        public string AccountNumber { get; set; }
        public DateTime Expiry {  get; set; }
        public bool IsDefault { get; set; }
        public int CustomerId { get; set; }
    }
}
