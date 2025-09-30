using Review.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Review.ApplicationCore.Contracts.Repositories
{
    public interface ICustomerReviewRepository : IRepository<CustomerReview>
    {
        IEnumerable<CustomerReview> GetByUserId(int userId);
        IEnumerable<CustomerReview> GetByProductId(int productId);
        IEnumerable<CustomerReview> GetByYear(int year);
        CustomerReview Approve(int id);
        CustomerReview Reject(int id);
    }
}
