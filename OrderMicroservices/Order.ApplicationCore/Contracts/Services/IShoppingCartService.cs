using Order.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.ApplicationCore.Contracts.Services
{
    public interface IShoppingCartService
    {
        ShoppingCart GetShoppingCartByCustomerId(int customerId);
        ShoppingCart SaveShoppingCart(ShoppingCart cart);
        bool DeleteShoppingCart(int id);
    }
}
