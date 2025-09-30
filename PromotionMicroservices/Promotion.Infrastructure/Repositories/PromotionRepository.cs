using Microsoft.EntityFrameworkCore;
using Promotion.ApplicationCore.Contracts.Repositories;
using Promotion.ApplicationCore.Entities;
using Promotion.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promotion.Infrastructure.Repositories
{
    public class PromotionRepository : IPromotionRepository
    {
        private readonly PromotionDbContext _dbContext;
        public PromotionRepository(PromotionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public PromotionEntity DeleteById(int id)
        {
            var e = _dbContext.Promotions.Find(id) ?? throw new KeyNotFoundException("Promotion not found");
            _dbContext.Promotions.Remove(e);
            _dbContext.SaveChanges();
            return e;
        }

        public bool Exists(int id)
        {
            return _dbContext.Promotions.Any(p => p.Id == id);
        }

        public IEnumerable<PromotionEntity> GetActive(DateTime asOf)
        {
            return _dbContext.Promotions
               .AsNoTracking()
               .Include(p => p.Details)
               .Where(p => p.StartDate <= asOf && p.EndDate >= asOf)
               .OrderBy(p => p.EndDate)
               .ToList();
        }

        public IEnumerable<PromotionEntity> GetAll()
        {
            return _dbContext.Promotions
               .AsNoTracking()
               .Include(p => p.Details)
               .OrderByDescending(p => p.StartDate)
               .ToList();
        }

        public PromotionEntity GetById(int id)
        {
            var entity = _dbContext.Promotions
               .AsNoTracking()
               .Include(p => p.Details)
               .FirstOrDefault(p => p.Id == id);
            if(entity == null)
            {
                return null;
            }
            return entity;
        }

        public IEnumerable<PromotionEntity> GetByProductName(string productName)
        {
            if (string.IsNullOrWhiteSpace(productName)) return Enumerable.Empty<PromotionEntity>();

            var name = productName.Trim();

            return _dbContext.Promotions
                      .AsNoTracking()
                      .Include(p => p.Details)
                      .Where(p => p.Details.Any(d =>
                          EF.Functions.Like(d.ProductCategoryName, $"%{name}%")))
                      .OrderByDescending(p => p.StartDate)
                      .ToList();
        }

        public PromotionEntity Insert(PromotionEntity entity)
        {
            throw new NotImplementedException();
        }

        public PromotionEntity Update(PromotionEntity entity)
        {
            var existing = _dbContext.Promotions
                .Include(p => p.Details)
                .FirstOrDefault(p => p.Id == entity.Id)
                ?? throw new KeyNotFoundException("Promotion not found");

            existing.Name = entity.Name;
            existing.Description = entity.Description;
            existing.Discount = entity.Discount;
            existing.StartDate = entity.StartDate;
            existing.EndDate = entity.EndDate;

            if (entity.Details != null)
            {
                _dbContext.PromotionDetails.RemoveRange(existing.Details);
                existing.Details = entity.Details;
            }

            _dbContext.SaveChanges();
            return existing;
        }
    }
}
