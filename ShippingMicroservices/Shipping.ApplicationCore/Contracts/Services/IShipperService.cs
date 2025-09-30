using Shipping.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.ApplicationCore.Contracts.Services
{
    public interface IShipperService
    {
        IEnumerable<Shipper> GetList();
        Shipper? GetById(int id);
        Shipper Save(Shipper shipper);
        Shipper Update(Shipper shipper);
        Shipper Delete(int id);

        IEnumerable<Shipper> GetByRegion(string region);
    }
}
