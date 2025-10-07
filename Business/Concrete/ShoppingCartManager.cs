using Business.Abstract;
using Business.Constants;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DTOs.DTOs.ShoppingCartDtos;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ShoppingCartManager : IShoppingCartService
    {
        private readonly IShoppingCartDal shoppingCartDal;
        private readonly ICacheService cacheService;

        public ShoppingCartManager(IShoppingCartDal shoppingCartDal, ICacheService cacheService)
        {
            this.shoppingCartDal = shoppingCartDal;
            this.cacheService = cacheService;
        }

        private string GetCacheKey(int userId) => $"cart:user:{userId}";

        public async Task<IResult> AddToCartAsync(AddToCartDto addToCart)
        {
            //kullanıcının sepetini getir
            var cart = await shoppingCartDal.GetCartByUserIdAsync(addToCart.UserId);

            if (cart is null)
            {
                //eğer sepet yoksa yeni oluştur
                cart = new ShoppingCart
                {
                    AuthUserId = addToCart.UserId,
                    Items = new List<ShoppingCartItem>()
                    {
                        new ShoppingCartItem {
                            ProductId = addToCart.ProductId,
                            Quantity = addToCart.Quantity,

                        }
                    }
                };
                await shoppingCartDal.AddAsync(cart);
                await cacheService.RemoveAsync(GetCacheKey(addToCart.UserId));
                return new SuccessResult(CartMessages.ProductAddedToNewCart);
            }

            // sepet varsa item kontrolü yap
            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == addToCart.ProductId);
            if (existingItem is not null)
            {
                existingItem.Quantity += addToCart.Quantity;

            }
            else
            {
                cart.Items.Add(new ShoppingCartItem
                {
                    ProductId = addToCart.ProductId,
                    Quantity = addToCart.Quantity,
                });
            }
            await shoppingCartDal.UpdateAsync(cart);

            //cache invalidate et
            await cacheService.RemoveAsync(GetCacheKey(addToCart.UserId));
            return new SuccessResult(CartMessages.ProductAddedToCart);
        }
        public async Task<IResult> ClearCartAsync(int userId)
        {
            var cart = await shoppingCartDal.GetCartByUserIdAsync(userId);
            if (cart is null)
                return new ErrorResult(CartMessages.CartNotFound);

            cart.Items.Clear();
            await shoppingCartDal.UpdateAsync(cart);

            await cacheService.RemoveAsync(GetCacheKey(userId));
            return new SuccessResult(CartMessages.CartCleared);
        }

        public async Task<IDataResult<List<ShoppingCartDetailDto>>> GetUserCartAsync(int userId)
        {
            var cacheKey = GetCacheKey(userId);

            // önce cache'e bak
            var cachedCart = await cacheService.GetAsync<List<ShoppingCartDetailDto>>(cacheKey);
            if (cachedCart != null)
                return new SuccessDataResult<List<ShoppingCartDetailDto>>(cachedCart);

            // cache'de yoksa db den al
            var cartDetails = await shoppingCartDal.GetCartDetailsByUserIdAsync(userId);

            //redis e yaz
            if (cartDetails != null && cartDetails.Any())
            {
                await cacheService.SetAsync(cacheKey, cartDetails, TimeSpan.FromMinutes(30));
                return new SuccessDataResult<List<ShoppingCartDetailDto>>(cartDetails, CartMessages.ProductAddedToNewCart);
            }
            return new ErrorDataResult<List<ShoppingCartDetailDto>>(CartMessages.CartIsEmpty);
        }

        public async Task<IResult> RemoveFromCartAsync(int userId, int productId)
        {
            var cart = await shoppingCartDal.GetCartByUserIdAsync(userId);
            if (cart is null)
                return new ErrorResult(CartMessages.CartNotFound);

            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item is null)
                return new ErrorResult(CartMessages.ProductNotFoundInCart);

            cart.Items.Remove(item);
            await shoppingCartDal.UpdateAsync(cart);

            await cacheService.RemoveAsync(GetCacheKey(userId));
            return new SuccessResult(CartMessages.ProductRemovedFromCart);
        }

        public async Task<IResult> UpdateQuantityAsync(UpdateCartDto updateCart)
        {
            var cart = await shoppingCartDal.GetCartByUserIdAsync(updateCart.UserId);
            if (cart is null)
                return new ErrorResult(CartMessages.CartNotFound);

            var item = cart.Items.FirstOrDefault(i => i.ProductId == updateCart.ProductId);
            if (item is null)
                return new ErrorResult(CartMessages.ProductNotFoundInCart);

            item.Quantity = updateCart.Quantity;
            await shoppingCartDal.UpdateAsync(cart);
            await cacheService.RemoveAsync(GetCacheKey(updateCart.UserId));
            return new SuccessResult(CartMessages.CartQuantityUpdated);
        }
    }
}
