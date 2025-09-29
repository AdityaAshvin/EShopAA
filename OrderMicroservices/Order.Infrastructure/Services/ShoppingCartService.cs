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
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _carts;
        public ShoppingCartService(IShoppingCartRepository carts)
        {
            _carts = carts;
        }
        public bool DeleteShoppingCart(int id)
        {
            throw new NotImplementedException();
        }

        public ShoppingCart GetShoppingCartByCustomerId(int customerId)
        {
            if (customerId <= 0) return null!;
            return _carts.GetByCustomerId(customerId);
        }

        public ShoppingCart SaveShoppingCart(ShoppingCart cart)
        {
            if (cart == null) return null!;
            cart.Items ??= new List<ShoppingCartItem>();
            if (cart.Items.Any())
            {
                cart.Items = [.. cart.Items
                    .Where(i => i.Qty > 0 && i.Price >= 0)
                    .GroupBy(i => i.ProductId)
                    .Select(g => new ShoppingCartItem
                    {
                        ProductId = g.Key,
                        ProductName = g.First().ProductName,
                        Qty = g.Sum(x => x.Qty),
                        Price = g.First().Price
                    })];
            }

            return _carts.SaveCart(cart);
        }
    }
}
