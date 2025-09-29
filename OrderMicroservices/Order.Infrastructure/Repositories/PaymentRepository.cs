using Microsoft.EntityFrameworkCore;
using Order.ApplicationCore.Contracts.Repositories;
using Order.ApplicationCore.Entities;
using Order.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.Repositories
{
    public class PaymentRepository : BaseRepository<PaymentMethod>, IPaymentRepository
    {
        private readonly EShopDbContext _dbContext;
        public PaymentRepository(EShopDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<PaymentMethod> GetByCustomerId(int customerId)
        {
            return _dbContext.PaymentMethods
                .AsNoTracking()
                .Include(p => p.PaymentType)
                .Where(p => p.CustomerId == customerId)
                .OrderByDescending(p => p.IsDefault)
                .ThenBy(p => p.Id)
                .ToList();
        }
    }
}
