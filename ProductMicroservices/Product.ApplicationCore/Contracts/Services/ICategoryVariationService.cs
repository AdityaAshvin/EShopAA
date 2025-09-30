using Product.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.ApplicationCore.Contracts.Services
{
    public interface ICategoryVariationService
    {
        IEnumerable<CategoryVariation> GetAll();
        CategoryVariation? GetById(int id);
        IEnumerable<CategoryVariation> GetByCategoryId(int categoryId);
        CategoryVariation Save(CategoryVariation model);
        bool Delete(int id);
    }
}
