using Product.ApplicationCore.Contracts.Repositories;
using Product.ApplicationCore.Contracts.Services;
using Product.ApplicationCore.Entities;
using Product.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure.Services
{
    public class CategoryVariationService : ICategoryVariationService
    {
        private readonly ICategoryVariationRepository _variations;
        private readonly IRepository<ProductCategory> _categories;
        private readonly ProductDbContext _dbContext;

        public CategoryVariationService(ICategoryVariationRepository variations, IRepository<ProductCategory> categories, ProductDbContext dbContext)
        {
            _variations = variations;
            _categories = categories;
            _dbContext = dbContext;
        }
        public bool Delete(int id)
        {
            var used = _dbContext.VariationValues.Any(v => v.VariationId == id);
            if (used) return false;

            _variations.DeleteById(id);
            return true;
        }

        public IEnumerable<CategoryVariation> GetAll()
        {
            return _variations.GetAll();
        }

        public IEnumerable<CategoryVariation> GetByCategoryId(int categoryId)
        {
            return _variations.GetByCategoryId(categoryId);
        }

        public CategoryVariation? GetById(int id)
        {
            return _variations.GetById(id);
        }

        public CategoryVariation Save(CategoryVariation model)
        {
            if (string.IsNullOrWhiteSpace(model.VariationName))
                throw new System.ArgumentException("VariationName missing.");
            if (!_categories.Exists(model.CategoryId))
                throw new System.ArgumentException("no such category.");

            var name = model.VariationName.Trim();
            bool duplicate = _dbContext.CategoryVariations
                .Any(v => v.CategoryId == model.CategoryId &&
                          v.VariationName.ToLower() == name.ToLower() &&
                          v.Id != model.Id);

            if (duplicate)
                throw new System.ArgumentException("A variation with the same name already exists in this category.");

            if (model.Id == 0)
            {
                model.VariationName = name;
                return _variations.Insert(model);
            }

            var existing = _variations.GetById(model.Id)
                           ?? throw new System.ArgumentException("Variation not found.");

            existing.VariationName = name;
            existing.CategoryId = model.CategoryId;

            return _variations.Update(existing)!;
        }
    }
}
