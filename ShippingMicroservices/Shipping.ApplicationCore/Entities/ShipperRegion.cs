using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.ApplicationCore.Entities
{
    public class ShipperRegion
    {
        public Region Region {  get; set; }
        public int RegionId { get; set; }

        public Shipper Shipper {  get; set; }
        public int ShipperId { get; set; }

        public bool Active { get; set; }
    }
}
