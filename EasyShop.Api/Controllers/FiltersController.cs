using Business.Abstract;
using DTOs.DTOs.FilterDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FiltersController : ControllerBase
    {
        private readonly IFilterService filterService;

        public FiltersController(IFilterService filterService)
        {
            this.filterService = filterService;
        }

        [HttpGet("colors")]
        public async Task<IActionResult> GetColors()
        {
            var colors= await filterService.GetColorListAsync();
            return Ok(colors);
        }


        [HttpGet("components")]
        public async Task<IActionResult> GetComponents()
        {
            var components = await filterService.GetComponentListAsync();
            return Ok(components);
        }


        [HttpGet("price-range")]
        public async Task<IActionResult> GetPriceRange()
        {
            var priceRange = await filterService.GetPriceRangeListAsync();
            return Ok(priceRange);
        }


        [HttpGet("sizes")]
        public async Task<IActionResult> GetSizes()
        {
            var sizes = await filterService.GetSizeListAsync();
            return Ok(sizes);
        }

        [HttpPost("filtered-products")]
        public async Task<IActionResult> GetFilteredProducts([FromBody] FilterRequestDto filterRequest)
        {
            var filteredProducts = await filterService.GetFilteredProductsListAsync(filterRequest);
            return Ok(filteredProducts);
        }
    }
}
