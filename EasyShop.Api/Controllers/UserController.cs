using Business.Abstract;
using Core.Extensions;
using DTOs.DTOs.UserDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPut("user-profile")]
        public async Task<IActionResult> UpdateProfileAsync([FromBody] UserProfileUpdateDto userForUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.GetUserId(); // extension'dan çağırıyoruz

            var result = await _userService.UpdateUserAsync(userId, userForUpdateDto);

            if (result > 0)
            {
                return Ok(new { Message = "Bilgileriniz başarıyla güncellendi." });
            }

            return BadRequest(new { Message = "Güncelleme başarısız" });

        }
    }
}
