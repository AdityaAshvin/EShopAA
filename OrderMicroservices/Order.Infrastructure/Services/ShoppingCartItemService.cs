using Order.ApplicationCore.Contracts.Repositories;
using Order.ApplicationCore.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.Services
{
    public class ShoppingCartItemService : IShoppingCartItemService
    {
        private readonly IShoppingCartItemRepository _items;

        public ShoppingCartItemService(IShoppingCartItemRepository items)
        {
            _items = items;
        }
        public bool DeleteShoppingCartItemById(int id)
        {
            var existing = _items.GetById(id);
            if (existing == null) return false;

            _items.DeleteById(id);
            return true;
        }
    }
}
