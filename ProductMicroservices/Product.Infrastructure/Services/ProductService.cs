using Product.ApplicationCore.Contracts.Repositories;
using Product.ApplicationCore.Contracts.Services;
using Product.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        public ProductEntity Delete(int id)
        {
            return _repo.DeleteById(id);
        }

        public IEnumerable<ProductEntity> GetByCategoryId(int categoryId)
        {
            return _repo.GetByCategoryId(categoryId);
        }

        public ProductEntity? GetById(int id)
        {
            return _repo.GetById(id);
        }

        public IEnumerable<ProductEntity> GetByName(string name)
        {
            return _repo.GetByName(name);
        }

        public IEnumerable<ProductEntity> GetList()
        {
            return _repo.GetAll();
        }

        public ProductEntity InActive(int id)
        {
            return _repo.SetInactive(id);
        }

        public ProductEntity Save(ProductEntity product)
        {
            return _repo.Insert(product);
        }

        public ProductEntity Update(ProductEntity product)
        {
            return _repo.Update(product);
        }
    }
}
