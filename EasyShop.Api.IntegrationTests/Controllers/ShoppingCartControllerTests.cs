using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Concrete.EntityFramework;
using DTOs.DTOs.ShoppingCartDtos;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;

namespace EasyShop.Api.IntegrationTests.Controllers
{
    public class ShoppingCartControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;
        private readonly CustomWebApplicationFactory _factory;

        public ShoppingCartControllerTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _factory.ResetDatabase(_factory.Services); // her testten önce db sıfırlama - test izolasyonu için
            _httpClient = factory.CreateClient();
        }


        [Fact(DisplayName = "Sepet boşken GetUserCart çağrıldığında boş dönmeli")]
        public async Task GetUserCart_Should_Return_Empty_When_No_Cart()
        {

            var userId = 1;

            var response = await _httpClient.GetAsync($"/api/ShoppingCart/{userId}");

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var result = await response.Content.ReadFromJsonAsync<Result>();
            result.Should().NotBeNull();
            result!.Success.Should().BeFalse();
            result.Message.Should().Be(CartMessages.CartIsEmpty);

        }

        [Fact(DisplayName = "Yeni ürün sepete eklendiğinde yeni sepet oluşturulmalı")]
        public async Task AddToCart_Should_Create_New_Cart_When_No_Existing_Cart()
        {

            var dto = new AddToCartDto { UserId = 1, ProductId = 1, Quantity = 2 };

            var response = await _httpClient.PostAsJsonAsync("/api/ShoppingCart", dto);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.ReadFromJsonAsync<Result>();
            result.Should().NotBeNull();
            result!.Success.Should().BeTrue();
            result.Message.Should().Be(CartMessages.ProductAddedToNewCart);


            // db doğrulaması
            using var scope = _factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<Context>();
            var cart = await db.ShoppingCarts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.AuthUserId == dto.UserId);

            cart.Should().NotBeNull();
            cart!.Items.Should().ContainSingle(i => i.ProductId == dto.ProductId && i.Quantity == dto.Quantity);
        }

        [Fact(DisplayName = "Aynı ürün tekrar sepete eklendiğinde miktar güncellenmeli")]
        public async Task AddToCart_Should_Update_Quantity_When_Product_Already_Exists()
        {

            var dto = new AddToCartDto { UserId = 2, ProductId = 2, Quantity = 1 };
            await _httpClient.PostAsJsonAsync("/api/ShoppingCart", dto);

            //Act - aynı ürünğ tekrar ekle
            dto.Quantity = 3;
            var response = await _httpClient.PostAsJsonAsync("/api/ShoppingCart", dto);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.ReadFromJsonAsync<Result>();
            result.Should().NotBeNull();
            result!.Success.Should().BeTrue();
            result.Message.Should().Be(CartMessages.ProductAddedToCart);


            using var scope = _factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<Context>();
            var cart = await db.ShoppingCarts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.AuthUserId == dto.UserId);

            var item = cart!.Items.FirstOrDefault(i => i.ProductId == dto.ProductId);
            item!.Quantity.Should().Be(4);

        }

        [Fact(DisplayName = "Farklı ürün sepete eklendiğinde yeni item eklenmeli")]
        public async Task AddToCart_Should_Add_New_Item_When_Product_Is_Different()
        {

            var dto1 = new AddToCartDto { UserId = 3, ProductId = 1, Quantity = 1 };
            var dto2 = new AddToCartDto { UserId = 3, ProductId = 2, Quantity = 5 };

            await _httpClient.PostAsJsonAsync("/api/ShoppingCart", dto1);

            var response = await _httpClient.PostAsJsonAsync("/api/ShoppingCart", dto2);

            response.StatusCode.Should().Be(HttpStatusCode.OK);


            var result = await response.Content.ReadFromJsonAsync<Result>();
            result.Should().NotBeNull();
            result!.Success.Should().BeTrue();
            result.Message.Should().Be(CartMessages.ProductAddedToCart);

            using var scope = _factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<Context>();
            var cart = await db.ShoppingCarts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.AuthUserId == dto1.UserId);

            cart!.Items.Should().HaveCount(2);
        }

        [Fact(DisplayName = "Sepetteki ürün miktarı güncellenmeli")]
        public async Task UpdateQuantity_Should_Update_Item_Quantity()
        {

            var addDto = new AddToCartDto { UserId = 1, ProductId = 3, Quantity = 2 };
            await _httpClient.PostAsJsonAsync("/api/ShoppingCart", addDto);

            var updateDto = new UpdateCartDto { UserId = 1, ProductId = 3, Quantity = 10 };

            var response = await _httpClient.PutAsJsonAsync("/api/ShoppingCart/update-quantity", updateDto);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.ReadFromJsonAsync<Result>();
            result.Should().NotBeNull();
            result!.Success.Should().BeTrue();
            result.Message.Should().Be(CartMessages.CartQuantityUpdated);

            using var scope = _factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<Context>();
            var updatedItem = await db.ShoppingCartItems
                .Include(i => i.Cart)
                .FirstOrDefaultAsync(i => i.Cart.AuthUserId == updateDto.UserId && i.ProductId == updateDto.ProductId);

            updatedItem!.Quantity.Should().Be(10);
        }

        [Fact(DisplayName = "Sepetten ürün silinmeli")]
        public async Task RemoveFromCart_Should_Remove_Product()
        {
            var addDto = new AddToCartDto { UserId = 2, ProductId = 1, Quantity = 1 };
            await _httpClient.PostAsJsonAsync("/api/ShoppingCart", addDto);

            using var scope = _factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<Context>();
            var cartItem = db.ShoppingCartItems
                             .Include(i => i.Cart)
                             .FirstOrDefault(i => i.Cart.AuthUserId == 2 && i.ProductId == 1);

            cartItem.Should().NotBeNull("Sepete ürün eklenmiş olmalı");

            var response = await _httpClient.DeleteAsync($"/api/ShoppingCart/{cartItem!.Cart.AuthUserId}/items/{cartItem.ProductId}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.ReadFromJsonAsync<Result>();
            result.Should().NotBeNull();
            result!.Success.Should().BeTrue();
            result.Message.Should().Be(CartMessages.ProductRemovedFromCart);


            using var scope2 = _factory.Services.CreateScope();
            var dbCheck = scope2.ServiceProvider.GetRequiredService<Context>();
            var stillExists = await dbCheck.ShoppingCartItems
                .AnyAsync(i => i.Cart.AuthUserId == addDto.UserId && i.ProductId == addDto.ProductId);

            stillExists.Should().BeFalse();
        }

        [Fact(DisplayName = "Sepeti tamamen boşaltmalı")]
        public async Task ClearCart_Should_Remove_All_Items()
        {
            var dto1 = new AddToCartDto { UserId = 3, ProductId = 2, Quantity = 2 };
            var dto2 = new AddToCartDto { UserId = 3, ProductId = 3, Quantity = 1 };
            await _httpClient.PostAsJsonAsync("/api/ShoppingCart", dto1);
            await _httpClient.PostAsJsonAsync("/api/ShoppingCart", dto2);


            var userId = 3;
            var response = await _httpClient.DeleteAsync($"/api/ShoppingCart/{userId}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.ReadFromJsonAsync<Result>();
            result.Should().NotBeNull();
            result!.Success.Should().BeTrue();
            result.Message.Should().Be(CartMessages.CartCleared);



            using var scope = _factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<Context>();
            var cartItems = db.ShoppingCartItems
                              .Include(i => i.Cart)
                              .Where(i => i.Cart.AuthUserId == userId)
                              .ToList();
            cartItems.Should().BeEmpty("Sepet tamamen temizlenmiş olmalı");
        }


        [Fact(DisplayName = "Mevcut olmayan sepetin miktarı güncellenmeye çalışılırsa hata dönmeli")]
        public async Task UpdateQuantity_Should_Return_Error_When_Cart_Not_Found()
        {
            var dto = new UpdateCartDto { UserId = 999, ProductId = 1, Quantity = 5 };
            var response = await _httpClient.PutAsJsonAsync("/api/ShoppingCart/update-quantity", dto);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var result = await response.Content.ReadFromJsonAsync<Result>();
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Be(CartMessages.CartNotFound);

            using var scope = _factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<Context>();
            var cart = await db.ShoppingCarts.FirstOrDefaultAsync(c => c.AuthUserId == dto.UserId);
            cart.Should().BeNull();
        }

        [Fact(DisplayName = "Sepetten olmayan ürün silinmeye çalışılırsa hata dönmeli")]
        public async Task RemoveFromCart_Should_Return_Error_When_Product_Not_In_Cart()
        {
            var userId = 5;
            var addDto = new AddToCartDto { UserId = userId, ProductId = 10, Quantity = 1 };
            await _httpClient.PostAsJsonAsync("/api/ShoppingCart", addDto);

            var response = await _httpClient.DeleteAsync($"/api/ShoppingCart/{userId}/items/999");

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var result = await response.Content.ReadFromJsonAsync<Result>();
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Be(CartMessages.ProductNotFoundInCart);


            using var scope = _factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<Context>();
            var cart = await db.ShoppingCarts.Include(c => c.Items).FirstOrDefaultAsync(c => c.AuthUserId == userId);
            cart!.Items.Should().ContainSingle(i => i.ProductId == 10);
        }

        [Fact(DisplayName = "Sepet boşken temizleme işlemi hata dönmeli")]
        public async Task ClearCart_Should_Return_Error_When_Cart_Is_Empty()
        {
            var userId = 10;
            var response = await _httpClient.DeleteAsync($"/api/ShoppingCart/{userId}");

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var result = await response.Content.ReadFromJsonAsync<Result>();
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Be(CartMessages.CartNotFound);

            using var scope = _factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<Context>();
            var cartExists = await db.ShoppingCarts.AnyAsync(c => c.AuthUserId == userId);
            cartExists.Should().BeFalse();
        }
    }
}

