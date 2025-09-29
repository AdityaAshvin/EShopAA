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
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _categories;
        private readonly ProductDbContext _dbContext;

        public ProductCategoryService(IProductCategoryRepository categories, ProductDbContext dbContext)
        {
            _categories = categories;
            _dbContext = dbContext;
        }
        public bool Delete(int id)
        {
            var hasChildren = _dbContext.ProductCategories.Any(c => c.ParentCategoryId == id);
            if (hasChildren) return false;

            var hasProducts = _dbContext.Products.Any(p => p.CategoryId == id);
            if (hasProducts) return false;

            _categories.DeleteById(id);
            return true;
        }

        public IEnumerable<ProductCategory> GetAllCategory()
        {
            return _categories.GetAll();
        }

        public ProductCategory? GetCategoryById(int id)
        {
            return _categories.GetById(id);
        }

        public IEnumerable<ProductCategory> GetCategoryByParentCategoryId(int? parentCategoryId)
        {
            return _categories.GetByParentCategoryId(parentCategoryId);
        }

        public ProductCategory SaveCategory(ProductCategory model)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
                throw new System.ArgumentException("Category name is required.");

            if (model.ParentCategoryId.HasValue && model.ParentCategoryId == model.Id)
                throw new System.ArgumentException("ParentCategoryId cannot be the same as the category Id.");

            if (model.ParentCategoryId.HasValue && model.ParentCategoryId.Value > 0)
            {
                var parentExists = _dbContext.ProductCategories.Any(c => c.Id == model.ParentCategoryId.Value);
                if (!parentExists) throw new System.ArgumentException("Parent category not found.");
            }

            if (model.Id == 0)
            {
                return _categories.Insert(model);
            }

            var existing = _categories.GetById(model.Id);
            if (existing == null) throw new System.ArgumentException("Category not found.");

            existing.Name = model.Name.Trim();
            existing.ParentCategoryId = model.ParentCategoryId;

            return _categories.Update(existing)!;
        }
    }
}
