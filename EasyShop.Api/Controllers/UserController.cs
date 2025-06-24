using Business.Abstract;
using Business.Constants;
using Core.Extensions;
using Core.Utilities.Exceptions;
using Core.Utilities.Results;
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

        [HttpPut("update-profile")]
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

                return BadRequest(new { Errors = errors });
            }

            var userId = User.GetUserId(); // extension'dan çağırıyoruz

            var result = await _userService.UpdateUserAsync(userId, userForUpdateDto);

            if (result > 0)
            {
                return Ok(new { Success = true, Message = "Bilgileriniz başarıyla güncellendi." });

            }

            return BadRequest(new { Success = false, Message = "Güncelleme başarısız" });

        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordDto changePassword)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                return BadRequest(new { Errors = errors });
            }
            var userId = User.GetUserId();
            var result = await _userService.ChangePasswordAsync(userId, changePassword);

            if (!result.Success)
                return BadRequest(new { Success = false, Message = result.Message });

            return Ok(result);
        }

    }
}
