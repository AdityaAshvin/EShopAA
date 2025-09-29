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
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        private readonly EShopDbContext _dbContext;
        public CustomerRepository(EShopDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<UserAddress> GetAddressesByUserId(string userId)
        {
            var customer = _dbContext.Customers
                             .Include(c => c.UserAddresses)
                             .ThenInclude(ua => ua.Address)
                             .FirstOrDefault(c => c.UserId == userId);

            if(customer == null)
                return Enumerable.Empty<UserAddress>();
            else
                return customer.UserAddresses;
        }

        public Customer? GetByUserId(string userId)
        {
            return _dbContext.Customers
                .Include(c => c.UserAddresses)
                    .ThenInclude(ua => ua.Address)
                .AsNoTracking()
                .FirstOrDefault(c => c.UserId == userId);
        }

        public UserAddress InsertCustomerAddress(UserAddress address)
        {
            _dbContext.UserAddresses.Add(address);
            _dbContext.SaveChanges();
            return address;
        }
    }
}
