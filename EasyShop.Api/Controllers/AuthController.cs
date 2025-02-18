using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Exceptions;
using DTOs.DTOs.UserDtos;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace EasyShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            //try-catch gerek yok, global exception handler var

            var userExist = await _authService.UserExists(userForRegisterDto.Email);
            if (userExist)
            {
                throw new UserAlreadyExistsException(Messages.UserAlreadyExists);
            }
            //yeni kullanıcı
            var registerResult = await _authService.RegisterAsync(userForRegisterDto, userForRegisterDto.Password);
            if(!registerResult.Success)
            {
                return BadRequest(registerResult.Message);
            }
            return Ok(registerResult.Data);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = await _authService.LoginAsync(userForLoginDto);
            if(!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }
            return Ok(userToLogin.Data);
        }

    }
}

