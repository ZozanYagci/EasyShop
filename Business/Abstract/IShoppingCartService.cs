using Core.Utilities.Results;
using DTOs.DTOs.ShoppingCartDtos;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IShoppingCartService 
    {
        Task<IResult> AddToCartAsync(AddToCartDto addToCart);
        Task<IResult> RemoveFromCartAsync(int userId, int productId);
        Task<IResult> UpdateQuantityAsync(UpdateCartDto updateCart);
        Task<IDataResult<List<ShoppingCartDetailDto>>> GetUserCartAsync(int userId);
        Task<IResult> ClearCartAsync(int userId);
    }
}
