using Order.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.ApplicationCore.Contracts.Repositories
{
    public interface IShoppingCartItemRepository : IRepository<ShoppingCartItem>
    {
    }
}
