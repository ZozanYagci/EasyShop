using Business.Abstract;
using DTOs.DTOs.ProductImageDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImagesController : ControllerBase
    {
        private readonly IProductImageService productImageService;

        public ProductImagesController(IProductImageService productImageService)
        {
            this.productImageService = productImageService;
        }


        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] ProductImageUploadDto uploadDto)
        {
            var result = await productImageService.UploadImagesAsync(uploadDto);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetImagesByProductId(int productId)
        {
            var result = await productImageService.GetImagesByProductIdAsync(productId);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await productImageService.RemoveImageAsync(id);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

    }
}
