using Product.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.ApplicationCore.Contracts.Services
{
    public interface IProductService
    {
        IEnumerable<ProductEntity> GetList();
        ProductEntity? GetById(int id);
        IEnumerable<ProductEntity> GetByCategoryId(int categoryId);
        IEnumerable<ProductEntity> GetByName(string name);
        ProductEntity Save(ProductEntity product);
        ProductEntity Update(ProductEntity product);
        ProductEntity InActive(int id);
        ProductEntity Delete(int id);
    }
}
