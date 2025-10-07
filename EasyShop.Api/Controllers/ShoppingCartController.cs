using Business.Abstract;
using DTOs.DTOs.ShoppingCartDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserCart(int userId)
        {
            var result = await _shoppingCartService.GetUserCartAsync(userId);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(AddToCartDto addToCartDto)
        {

            var result = await _shoppingCartService.AddToCartAsync(addToCartDto);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("{userId}/items/{productId}")]
        public async Task<IActionResult> RemoveFromCart(int userId, int productId)
        {
            var result = await _shoppingCartService.RemoveFromCartAsync(userId, productId);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPut("update-quantity")]
        public async Task<IActionResult> UpdateQuantity(UpdateCartDto updateCartDto)
        {
            var result = await _shoppingCartService.UpdateQuantityAsync(updateCartDto);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> ClearCart(int userId)
        {
            var result = await _shoppingCartService.ClearCartAsync(userId);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
