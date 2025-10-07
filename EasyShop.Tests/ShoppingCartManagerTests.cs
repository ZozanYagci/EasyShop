using Business.Concrete;
using Business.Constants;
using Core.CrossCuttingConcerns.Caching;
using DataAccess.Abstract;
using DTOs.DTOs.ShoppingCartDtos;
using Entities.Concrete;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyShop.Tests
{
    public class ShoppingCartManagerTests
    {
        private readonly Mock<IShoppingCartDal> _shoppingCartDalMock;
        private readonly Mock<ICacheService> _cacheServiceMock;
        private readonly ShoppingCartManager _shoppingCartManager;

        public ShoppingCartManagerTests()
        {
            _shoppingCartDalMock = new Mock<IShoppingCartDal>();
            _cacheServiceMock = new Mock<ICacheService>();
            _shoppingCartManager = new ShoppingCartManager(_shoppingCartDalMock.Object, _cacheServiceMock.Object);
        }

        [Fact]
        public async Task AddToCartAsync_Should_Create_New_Cart_When_User_Has_No_Cart()
        {
            //arrange
            var addToCartDto = new AddToCartDto
            {
                UserId = 1,
                ProductId = 10,
                Quantity = 2
            };
            _shoppingCartDalMock.Setup(x => x.GetCartByUserIdAsync(addToCartDto.UserId))
                .ReturnsAsync((ShoppingCart)null);

            //Act
            var result = await _shoppingCartManager.AddToCartAsync(addToCartDto);

            //Assert
            result.Success.Should().BeTrue();
            result.Message.Should().Be(CartMessages.ProductAddedToNewCart);

            _shoppingCartDalMock.Verify(x => x.AddAsync(It.IsAny<ShoppingCart>()), Times.Once);
            _cacheServiceMock.Verify(x => x.RemoveAsync($"cart:user:{addToCartDto.UserId}"), Times.Once);

        }
        [Fact]
        public async Task AddToCartAsync_Should_Increment_Quantity_When_Product_Already_Exists()
        {
            var dto = new AddToCartDto { UserId = 1, ProductId = 10, Quantity = 2 };
            var cart = new ShoppingCart
            {
                AuthUserId = dto.UserId,
                Items = new List<ShoppingCartItem> { new ShoppingCartItem { ProductId = 10, Quantity = 3 } }
            };

            _shoppingCartDalMock.Setup(x => x.GetCartByUserIdAsync(dto.UserId)).ReturnsAsync(cart);

            var result = await _shoppingCartManager.AddToCartAsync(dto);

            result.Success.Should().BeTrue();
            result.Message.Should().Be(CartMessages.ProductAddedToCart);
            cart.Items.Single(i => i.ProductId == dto.ProductId).Quantity.Should().Be(5);

            _cacheServiceMock.Verify(x => x.RemoveAsync($"cart:user:{dto.UserId}"), Times.Once);
        }
        [Fact]
        public async Task AddToCartAsync_Should_Add_New_Item_When_Product_Is_Different()
        {
            var dto = new AddToCartDto { UserId = 1, ProductId = 20, Quantity = 1 };
            var cart = new ShoppingCart
            {
                AuthUserId = dto.UserId,
                Items = new List<ShoppingCartItem> { new ShoppingCartItem { ProductId = 10, Quantity = 2 } }

            };
            _shoppingCartDalMock.Setup(x => x.GetCartByUserIdAsync(dto.UserId)).ReturnsAsync(cart);


            var result = await _shoppingCartManager.AddToCartAsync(dto);

            result.Success.Should().BeTrue();
            result.Message.Should().Be(CartMessages.ProductAddedToCart);
            cart.Items.Count.Should().Be(2);


            _cacheServiceMock.Verify(x => x.RemoveAsync($"cart:user:{dto.UserId}"), Times.Once);
        }
        [Fact]
        public async Task RemoveFromCartAsync_Should_Remove_Item()
        {
            int userId = 1, productId = 10;
            var cart = new ShoppingCart
            {
                AuthUserId = userId,
                Items = new List<ShoppingCartItem>
                {
                    new ShoppingCartItem { ProductId = productId, Quantity = 2 }
                }
            };

            _shoppingCartDalMock.Setup(x => x.GetCartByUserIdAsync(userId)).ReturnsAsync(cart);

            var result = await _shoppingCartManager.RemoveFromCartAsync(userId, productId);

            result.Success.Should().BeTrue();
            result.Message.Should().Be(CartMessages.ProductRemovedFromCart);
            cart.Items.Should().BeEmpty();

            _cacheServiceMock.Verify(x => x.RemoveAsync($"cart:user:{userId}"), Times.Once);
        }

        [Fact]
        public async Task RemoveFromCartAsync_Should_Return_Error_When_Cart_Not_Found()
        {
            int userId = 999, productId = 1;
            _shoppingCartDalMock.Setup(x => x.GetCartByUserIdAsync(userId)).ReturnsAsync((ShoppingCart)null);

            var result = await _shoppingCartManager.RemoveFromCartAsync(userId, productId);

            result.Success.Should().BeFalse();
            result.Message.Should().Be(CartMessages.CartNotFound);

            _cacheServiceMock.Verify(x => x.RemoveAsync(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task RemoveFromCartAsync_Should_Return_Error_When_Product_Not_In_Cart()
        {

            int userId = 1, productId = 999;
            var cart = new ShoppingCart { AuthUserId = userId, Items = new List<ShoppingCartItem>() };
            _shoppingCartDalMock.Setup(x => x.GetCartByUserIdAsync(userId)).ReturnsAsync(cart);


            var result = await _shoppingCartManager.RemoveFromCartAsync(userId, productId);


            result.Success.Should().BeFalse();
            result.Message.Should().Be(CartMessages.ProductNotFoundInCart);
            _cacheServiceMock.Verify(x => x.RemoveAsync(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task UpdateQuantityAsync_Should_Update_Item_Quantity()
        {

            var dto = new UpdateCartDto { UserId = 1, ProductId = 10, Quantity = 5 };
            var cart = new ShoppingCart
            {
                AuthUserId = dto.UserId,
                Items = new List<ShoppingCartItem> { new ShoppingCartItem { ProductId = 10, Quantity = 2 } }
            };

            _shoppingCartDalMock.Setup(x => x.GetCartByUserIdAsync(dto.UserId)).ReturnsAsync(cart);


            var result = await _shoppingCartManager.UpdateQuantityAsync(dto);


            result.Success.Should().BeTrue();
            result.Message.Should().Be(CartMessages.CartQuantityUpdated);
            cart.Items.First().Quantity.Should().Be(dto.Quantity);

            _cacheServiceMock.Verify(x => x.RemoveAsync($"cart:user:{dto.UserId}"), Times.Once);
        }


        [Fact]
        public async Task UpdateQuantityAsync_Should_Return_Error_When_Cart_Not_Found()
        {
            // Arrange
            var dto = new UpdateCartDto { UserId = 999, ProductId = 1, Quantity = 5 };
            _shoppingCartDalMock.Setup(x => x.GetCartByUserIdAsync(dto.UserId)).ReturnsAsync((ShoppingCart)null);

            // Act
            var result = await _shoppingCartManager.UpdateQuantityAsync(dto);

            // Assert
            result.Success.Should().BeFalse();
            result.Message.Should().Be(CartMessages.CartNotFound);
        }

        [Fact]
        public async Task UpdateQuantityAsync_Should_Return_Error_When_Product_Not_In_Cart()
        {

            var dto = new UpdateCartDto { UserId = 1, ProductId = 99, Quantity = 5 };
            var cart = new ShoppingCart { AuthUserId = dto.UserId, Items = new List<ShoppingCartItem>() };
            _shoppingCartDalMock.Setup(x => x.GetCartByUserIdAsync(dto.UserId)).ReturnsAsync(cart);


            var result = await _shoppingCartManager.UpdateQuantityAsync(dto);


            result.Success.Should().BeFalse();
            result.Message.Should().Be(CartMessages.ProductNotFoundInCart);
        }

        [Fact]
        public async Task GetUserCartAsync_Should_Return_Cached_Data_If_Available()
        {
            
            int userId = 1;
            var cachedData = new List<ShoppingCartDetailDto>
            {
                new ShoppingCartDetailDto { ProductId = 10, ProductName = "Test", Quantity = 2 }
            };

            _cacheServiceMock.Setup(x => x.GetAsync<List<ShoppingCartDetailDto>>($"cart:user:{userId}"))
                .ReturnsAsync(cachedData);

           
            var result = await _shoppingCartManager.GetUserCartAsync(userId);

            
            result.Success.Should().BeTrue();
            result.Data.Should().BeEquivalentTo(cachedData);

            _shoppingCartDalMock.Verify(x => x.GetCartDetailsByUserIdAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task GetUserCartAsync_Should_Fetch_From_Db_And_Set_Cache_When_Cache_Miss()
        {
            
            int userId = 1;
            _cacheServiceMock
                .Setup(x => x.GetAsync<List<ShoppingCartDetailDto>>($"cart:user:{userId}"))
                .ReturnsAsync((List<ShoppingCartDetailDto>)null);

            var dbData = new List<ShoppingCartDetailDto>
            {
                 new ShoppingCartDetailDto { ProductId = 20, ProductName = "DB Test", Quantity = 3 }
            };
            _shoppingCartDalMock
                .Setup(x => x.GetCartDetailsByUserIdAsync(userId))
                .ReturnsAsync(dbData);

            string capturedKey = null;
            List<ShoppingCartDetailDto> capturedValue = null;
            TimeSpan? capturedExpiration = null;

            _cacheServiceMock
                .Setup(x => x.SetAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<TimeSpan?>()))
                .Callback<string, object, TimeSpan?>((key, value, expiration) =>
                {
                    capturedKey = key;
                    capturedValue = value as List<ShoppingCartDetailDto>;
                    capturedExpiration = expiration;
                })
                .Returns(Task.CompletedTask);

           
            var result = await _shoppingCartManager.GetUserCartAsync(userId);

           
            result.Success.Should().BeTrue();
            result.Data.Should().BeEquivalentTo(dbData);
            capturedKey.Should().Be($"cart:user:{userId}");
            capturedValue.Should().BeEquivalentTo(dbData);
            capturedExpiration.Should().Be(TimeSpan.FromMinutes(30));

            _shoppingCartDalMock.Verify(x => x.GetCartDetailsByUserIdAsync(userId), Times.Once);
            _cacheServiceMock.Verify(x => x.SetAsync($"cart:user:{userId}", dbData, TimeSpan.FromMinutes(30)), Times.Once);

        }


        [Fact]
        public async Task ClearCartAsync_Should_Clear_Items_And_Remove_Cache()
        {
            
            int userId = 1;
            var cart = new ShoppingCart
            {
                AuthUserId = userId,
                Items = new List<ShoppingCartItem>
                {
                    new ShoppingCartItem { ProductId = 10, Quantity = 2 }
                }
            };

            _shoppingCartDalMock.Setup(x => x.GetCartByUserIdAsync(userId)).ReturnsAsync(cart);

            
            var result = await _shoppingCartManager.ClearCartAsync(userId);

            
            result.Success.Should().BeTrue();
            result.Message.Should().Be(CartMessages.CartCleared);
            cart.Items.Should().BeEmpty();

            _cacheServiceMock.Verify(x => x.RemoveAsync($"cart:user:{userId}"), Times.Once);
        }
    }
}