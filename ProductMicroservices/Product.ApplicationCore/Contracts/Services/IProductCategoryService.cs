using Product.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.ApplicationCore.Contracts.Services
{
    public interface IProductCategoryService
    {
        IEnumerable<ProductCategory> GetAllCategory();
        ProductCategory? GetCategoryById(int id);
        IEnumerable<ProductCategory> GetCategoryByParentCategoryId(int? parentCategoryId);
        ProductCategory SaveCategory(ProductCategory model);
        bool Delete(int id);
    }
}
