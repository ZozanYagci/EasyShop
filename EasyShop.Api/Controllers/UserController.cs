using Business.Abstract;
using Business.Constants;
using Core.Extensions;
using Core.Utilities.Exceptions;
using DTOs.DTOs.UserDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyShop.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("my-profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.GetUserId();
            var user = await _userService.GetByIdAsync(userId);

            return Ok(user);
        }

        [HttpPut("user-profile")]
        public async Task<IActionResult> UpdateProfileAsync([FromBody] UserProfileUpdateDto userForUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                return BadRequest(new { errors });
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
