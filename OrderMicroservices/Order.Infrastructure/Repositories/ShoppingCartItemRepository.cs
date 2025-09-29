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
    public class ShoppingCartItemRepository : BaseRepository<ShoppingCartItem>, IShoppingCartItemRepository
    {
        private readonly EShopDbContext _dbContext;
        public ShoppingCartItemRepository(EShopDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
