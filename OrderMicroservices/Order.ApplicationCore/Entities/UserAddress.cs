using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.ApplicationCore.Entities
{
    public class UserAddress
    {
        public int Id { get; set; }

        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        
        public Address Address { get; set; }
        public int AddressId { get; set; }
        public bool IsDefaultAddress { get; set; }

    }
}
