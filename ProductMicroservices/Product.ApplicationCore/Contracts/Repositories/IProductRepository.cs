using Product.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.ApplicationCore.Contracts.Repositories
{
    public interface IProductRepository : IRepository<ProductEntity>
    {
        IEnumerable<ProductEntity> GetByCategoryId(int categoryId);
        IEnumerable<ProductEntity> GetByName(string name);
        ProductEntity SetInactive(int id);
    }
}
