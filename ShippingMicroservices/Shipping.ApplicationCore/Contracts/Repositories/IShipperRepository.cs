using Shipping.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.ApplicationCore.Contracts.Repositories
{
    public interface IShipperRepository : IRepository<Shipper>
    {
        IEnumerable<Shipper> GetByRegionId(int regionId);
        IEnumerable<Shipper> GetByRegionName(string regionName);
    }
}
