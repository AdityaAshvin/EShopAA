using Microsoft.EntityFrameworkCore;
using Review.ApplicationCore.Contracts.Repositories;
using Review.ApplicationCore.Entities;
using Review.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Review.Infrastructure.Repositories
{
    public class CustomerReviewRepository : ICustomerReviewRepository
    {
        private readonly ReviewDbContext _dbContext;
        public CustomerReviewRepository(ReviewDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public CustomerReview Approve(int id)
        {
            var e = _dbContext.CustomerReviews.Find(id) ?? throw new KeyNotFoundException("Review not found");
            e.Status = "Approved";
            _dbContext.SaveChanges();
            return e;
        }

        public CustomerReview DeleteById(int id)
        {
            var e = _dbContext.CustomerReviews.Find(id) ?? throw new KeyNotFoundException("Review not found");
            _dbContext.CustomerReviews.Remove(e);
            _dbContext.SaveChanges();
            return e;
        }

        public bool Exists(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CustomerReview> GetAll()
        {
            return _dbContext.CustomerReviews.AsNoTracking().OrderByDescending(r => r.ReviewDate).ToList();
        }

        public CustomerReview GetById(int id)
        {
            return _dbContext.CustomerReviews.AsNoTracking().FirstOrDefault(r => r.Id == id)!;
        }

        public IEnumerable<CustomerReview> GetByProductId(int productId)
        {
            return _dbContext.CustomerReviews.AsNoTracking()
               .Where(r => r.ProductId == productId)
               .OrderByDescending(r => r.ReviewDate)
               .ToList();
        }

        public IEnumerable<CustomerReview> GetByUserId(int userId)
        {
            return _dbContext.CustomerReviews.AsNoTracking()
               .Where(r => r.CustomerId == userId)
               .OrderByDescending(r => r.ReviewDate)
               .ToList();
        }

        public IEnumerable<CustomerReview> GetByYear(int year)
        {
           return _dbContext.CustomerReviews.AsNoTracking()
               .Where(r => r.ReviewDate.Year == year)
               .OrderByDescending(r => r.ReviewDate)
               .ToList();
        }

        public CustomerReview Insert(CustomerReview entity)
        {
            _dbContext.CustomerReviews.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public CustomerReview Reject(int id)
        {
            var e = _dbContext.CustomerReviews.Find(id) ?? throw new KeyNotFoundException("Review not found");
            e.Status = "Rejected";
            _dbContext.SaveChanges();
            return e;
        }

        public CustomerReview Update(CustomerReview entity)
        {
            var e = _dbContext.CustomerReviews.Find(entity.Id) ?? throw new KeyNotFoundException("Review not found");
   
            e.CustomerId = entity.CustomerId;
            e.CustomerName = entity.CustomerName;
            e.OrderId = entity.OrderId;
            e.OrderDate = entity.OrderDate;
            e.ProductId = entity.ProductId;
            e.ProductName = entity.ProductName;
            e.RatingValue = entity.RatingValue;
            e.Comment = entity.Comment;
            e.ReviewDate = entity.ReviewDate;

            if (!string.IsNullOrWhiteSpace(entity.Status)) e.Status = entity.Status;

            _dbContext.SaveChanges();
            return e;
        }
    }
}
