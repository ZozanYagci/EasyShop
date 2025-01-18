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

        [HttpGet("filter-values")]

        public async Task<IActionResult> GetFilterValues()
        {
            var filterValues = await filterService.GetFilterValueAsync();
            return Ok(filterValues);
        }

        [HttpPost("filtered-products")]
        public async Task<IActionResult> GetFilteredProducts([FromBody] FilterRequestDto filterRequest)
        {
           
            var filteredProducts = await filterService.GetFilteredProductsListAsync(filterRequest);
            return Ok(filteredProducts);
        }
    }
}
