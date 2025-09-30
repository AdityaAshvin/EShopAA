using Shipping.ApplicationCore.Contracts.Repositories;
using Shipping.ApplicationCore.Contracts.Services;
using Shipping.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Infrastructure.Services
{
    public class ShipperService : IShipperService
    {
        private readonly IShipperRepository _repo;
        public ShipperService(IShipperRepository repo)
        {
            _repo = repo;
        }

        public Shipper Delete(int id)
        {
            return _repo.DeleteById(id);
        }

        public Shipper? GetById(int id)
        {
            return _repo.GetById(id);
        }

        public IEnumerable<Shipper> GetByRegion(string region)
        {
            return int.TryParse(region, out var regionId)
                ? _repo.GetByRegionId(regionId)
                : _repo.GetByRegionName(region);
        }

        public IEnumerable<Shipper> GetList()
        {
            return _repo.GetAll();
        }

        public Shipper Save(Shipper shipper)
        {
            return _repo.Insert(shipper);
        }

        public Shipper Update(Shipper shipper)
        {
            return _repo.Update(shipper);
        }
    }
}
