using Business.Abstract;
using DataAccess.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoriesController : ControllerBase
    {
        private readonly ISubCategoryService subCategoryService;

        public SubCategoriesController(ISubCategoryService subCategoryService)
        {
                this.subCategoryService = subCategoryService;
        }

        [HttpGet("GetAllSubCategory")]
        public async Task<IActionResult> SubCategoryList()
        {
            var values = await subCategoryService.GetAllAsync();
            return Ok(values);
        }

        [HttpGet("GetSubCategoryByCategoryId")]
        public async Task<IActionResult> SubCategoryListByCategoryId()
        {
            var values = await subCategoryService.GetSubCategoryByCategoryIdAsync();
            return Ok(values);
        }
    }
}
