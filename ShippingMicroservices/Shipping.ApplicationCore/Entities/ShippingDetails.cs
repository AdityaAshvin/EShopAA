using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.ApplicationCore.Entities
{
    public class ShippingDetails
    {
        public int Id { get; set; }
        
        public Shipper Shipper {get; set; }
        public int ShipperId { get; set; }

        public int OrderId { get; set; }

        public string ShippingStatus { get; set; }
        public int TrackingNumber { get; set; }

    }
}
