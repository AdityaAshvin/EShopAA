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
    public class ShoppingCartRepository : BaseRepository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly EShopDbContext _dbContext;
        public ShoppingCartRepository(EShopDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public bool DeleteCart(int id)
        {
            var existing = _dbContext.ShoppingCarts
                .Include(c => c.Items)
                .FirstOrDefault(c => c.Id == id);

            if (existing == null) return false;

            _dbContext.ShoppingCartItems.RemoveRange(existing.Items);
            _dbContext.ShoppingCarts.Remove(existing);
            _dbContext.SaveChanges();
            return true;
        }

        public ShoppingCart GetByCustomerId(int customerId)
        {
            var cart = _dbContext.ShoppingCarts
                .Include(c => c.Items)
                .FirstOrDefault(c => c.CustomerId == customerId);
            if (cart == null)
            {
                return null;
            }
            return cart;
        }

        public ShoppingCart SaveCart(ShoppingCart cart)
        {
            var existing = _dbContext.ShoppingCarts
                .Include(c => c.Items)
                .FirstOrDefault(c => c.CustomerId == cart.CustomerId);

            if (existing == null)
            {
                _dbContext.ShoppingCarts.Add(cart);
            }
            else
            {
                existing.CustomerName = cart.CustomerName;

                _dbContext.ShoppingCartItems.RemoveRange(existing.Items);
                existing.Items = cart.Items;
            }

            _dbContext.SaveChanges();
            return cart;
        }
    }
}
