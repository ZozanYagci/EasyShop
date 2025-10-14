using Core.DataAccess.EntityFramework;
using DTOs.DTOs.ShoppingCartDtos;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IShoppingCartDal : IGenericRepository<ShoppingCart>
    {
        Task<ShoppingCart?> GetCartByUserIdAsync(int userId);
        Task<List<ShoppingCartDetailDto>> GetCartDetailsByUserIdAsync(int userId);
        Task MergeCartItemAsync(int cartId, List<SyncCartItemDto> items);
    }
}
