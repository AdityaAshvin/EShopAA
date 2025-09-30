using Review.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Review.ApplicationCore.Contracts.Services
{
    public interface ICustomerReviewService
    {
        IEnumerable<CustomerReview> GetList();
        CustomerReview? GetById(int id);
        IEnumerable<CustomerReview> GetByUserId(int userId);
        IEnumerable<CustomerReview> GetByProductId(int productId);
        IEnumerable<CustomerReview> GetByYear(int year);

        CustomerReview Save(CustomerReview review);
        CustomerReview Update(CustomerReview review);
        CustomerReview Delete(int id);

        CustomerReview Approve(int id);
        CustomerReview Reject(int id);
    }
}
