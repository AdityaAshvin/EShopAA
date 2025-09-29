using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.ApplicationCore.Entities
{
    public class PaymentType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<PaymentMethod> PaymentMethods { get; set; }
    }
}
