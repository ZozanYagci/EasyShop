using Core.DataAccess.GenericRepository;
using DataAccess.Abstract;
using DTOs.DTOs.ShoppingCartDtos;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfShoppingCartDal : GenericRepository<ShoppingCart, Context>, IShoppingCartDal
    {
        private readonly Context _context;
        public EfShoppingCartDal(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<ShoppingCart?> GetCartByUserIdAsync(int userId)
        {
            return await _context.ShoppingCarts
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.AuthUserId == userId);
        }

        public async Task<List<ShoppingCartDetailDto>> GetCartDetailsByUserIdAsync(int userId)
        {
            return await _context.ShoppingCartItems
                .Where(i => i.Cart.AuthUserId == userId)
                .Include(i => i.Product)
                .Select(i => new ShoppingCartDetailDto
                {
                    ProductId = i.ProductId,
                    ProductName = i.Product.Name,
                    Quantity = i.Quantity,
                    Price = _context.ProductPrices
                    .Where(pp => pp.ProductId == i.ProductId)
                    .OrderByDescending(pp => pp.EffectiveDate)
                    .Select(pp => pp.Price).FirstOrDefault(),
                }).ToListAsync();
        }
    }
}
