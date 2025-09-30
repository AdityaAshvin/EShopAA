using Review.ApplicationCore.Contracts.Repositories;
using Review.ApplicationCore.Contracts.Services;
using Review.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Review.Infrastructure.Services
{
    public class CustomerReviewService : ICustomerReviewService
    {
        private readonly ICustomerReviewRepository _repo;
        public CustomerReviewService(ICustomerReviewRepository repo)
        {
            _repo = repo;
        }

        public CustomerReview Approve(int id)
        {
            return _repo.Approve(id);
        }

        public CustomerReview Delete(int id)
        {
            return _repo.DeleteById(id);
        }

        public CustomerReview? GetById(int id)
        {
            return _repo.GetById(id);
        }

        public IEnumerable<CustomerReview> GetByProductId(int productId)
        {
            return _repo.GetByProductId(productId);
        }

        public IEnumerable<CustomerReview> GetByUserId(int userId)
        {
            return _repo.GetByUserId(userId);
        }

        public IEnumerable<CustomerReview> GetByYear(int year)
        {
            return _repo.GetByYear(year);
        }

        public IEnumerable<CustomerReview> GetList()
        {
            return _repo.GetAll();
        }

        public CustomerReview Reject(int id)
        {
            return _repo.Reject(id);
        }

        public CustomerReview Save(CustomerReview review)
        {
            return _repo.Insert(review);
        }

        public CustomerReview Update(CustomerReview review)
        {
            return _repo.Update(review);
        }
    }
}
