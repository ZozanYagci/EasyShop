using Core.DataAccess.GenericRepository;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfShoppingCartItemDal : GenericRepository<ShoppingCartItem, Context>, IShoppingCartItemDal
    {
        private readonly Context _context;
        public EfShoppingCartItemDal(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<ShoppingCartItem?> GetCartItemAsync(int userId, int productId)
        {
            return await _context.ShoppingCartItems
                .Include(i => i.Cart)
                .FirstOrDefaultAsync(i => i.Cart.AuthUserId == userId && i.ProductId == productId);
        }

        public async Task<List<ShoppingCartItem>> GetItemsByCartIdAsync(int cartId)
        {
            return await _context.ShoppingCartItems
                .Where(i => i.CartId == cartId)
                .Include(i => i.Product)
                .ToListAsync();
        }
    }
}
