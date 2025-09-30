using Microsoft.EntityFrameworkCore;
using Shipping.ApplicationCore.Contracts.Repositories;
using Shipping.ApplicationCore.Entities;
using Shipping.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipping.Infrastructure.Repositories
{
    public class ShipperRepository : IShipperRepository
    {
        private readonly ShippingDbContext _dbContext;
        public ShipperRepository(ShippingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Shipper DeleteById(int id)
        {
            var e = _dbContext.Shippers.Find(id);
            if (e == null) throw new KeyNotFoundException("Shipper not found");
            _dbContext.Shippers.Remove(e);
            _dbContext.SaveChanges();
            return e;
        }

        public bool Exists(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Shipper> GetAll()
        {
            return _dbContext.Shippers.AsNoTracking().OrderBy(s => s.Name).ToList();
        }

        public Shipper GetById(int id)
        {
            return _dbContext.Shippers.AsNoTracking().FirstOrDefault(s => s.Id == id)!;
        }

        public IEnumerable<Shipper> GetByRegionId(int regionId)
        {
            var q =
                from sr in _dbContext.ShipperRegions.AsNoTracking()
                where sr.RegionId == regionId && sr.Active
                join s in _dbContext.Shippers.AsNoTracking() on sr.ShipperId equals s.Id
                select s;
            return q.Distinct().OrderBy(s => s.Name).ToList();
        }

        public IEnumerable<Shipper> GetByRegionName(string regionName)
        {
            var q =
                from r in _dbContext.Regions.AsNoTracking()
                where EF.Functions.Like(r.Name, regionName)
                join sr in _dbContext.ShipperRegions.AsNoTracking() on r.Id equals sr.RegionId
                where sr.Active
                join s in _dbContext.Shippers.AsNoTracking() on sr.ShipperId equals s.Id
                select s;
            return q.Distinct().OrderBy(s => s.Name).ToList();
        }

        public Shipper Insert(Shipper entity)
        {
            _dbContext.Shippers.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public Shipper Update(Shipper entity)
        {
            var existing = _dbContext.Shippers.Find(entity.Id)
                           ?? throw new KeyNotFoundException("Shipper not found");
            existing.Name = entity.Name;
            existing.EmailId = entity.EmailId;
            existing.Phone = entity.Phone;
            existing.ContactPerson = entity.ContactPerson;
            _dbContext.SaveChanges();
            return existing;
        }
    }
}
