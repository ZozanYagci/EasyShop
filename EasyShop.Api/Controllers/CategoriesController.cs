using Business.Abstract;
using DataAccess.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
                this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task <IActionResult> CategoryList()
        {
            var values = await categoryService.GetAllAsync();
            return Ok(values);
        }
    }
}
