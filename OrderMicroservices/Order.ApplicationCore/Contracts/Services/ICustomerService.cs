using Order.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.ApplicationCore.Contracts.Services
{
    public interface ICustomerService
    {
        Customer? GetByUserId(string userId);
        IEnumerable<UserAddress> GetAddressesByUserId(string userId);
        UserAddress SaveCustomerAddress(UserAddress address);
    }
}
