﻿using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products= await productService.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("GetProductStocks")]

        public async Task<IActionResult> GetProductStocks()
        {
            var productStocks= await productService.GetProductStockAsync();    
            return Ok(productStocks);
        }

        [HttpGet("GetRecentProduct")]

        public async Task<IActionResult> GetRecentProduct()
        {
            var recentProducts = await productService.GetRecentProductAsync();
            return Ok(recentProducts);
        }


        [HttpGet("ProductWithPrices")]

        public async Task<IActionResult> GetProductWithPrices()
        {
            var recentProducts = await productService.GetProductWithPricesAsync();
            return Ok(recentProducts);
        }

        [HttpGet("ProductDetail/{productId}")]

        public async Task<IActionResult> GetProductDetails(int productId)
        {
            var productDetails = await productService.GetProductDetailsAsync(productId);
            return Ok(productDetails);
        }
    }
}
