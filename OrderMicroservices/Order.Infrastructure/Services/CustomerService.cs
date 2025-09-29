using Order.ApplicationCore.Contracts.Repositories;
using Order.ApplicationCore.Contracts.Services;
using Order.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customers;
        public CustomerService(ICustomerRepository customers)
        {
            _customers = customers;
        }
        public IEnumerable<UserAddress> GetAddressesByUserId(string userId)
        {
            return _customers.GetAddressesByUserId(userId);
        }

        public Customer? GetByUserId(string userId)
        {
            return _customers.GetByUserId(userId);
        }

        public UserAddress SaveCustomerAddress(UserAddress address)
        {
            return _customers.InsertCustomerAddress(address);
        }
    }
}
