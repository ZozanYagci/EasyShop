using Business.Abstract;
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
                throw new UserAlreadyExistsException("Bu e-posta adresiyle kayıtlı bir kullanıcı zaten var.");
            }
            //yeni kullanıcı
            var registerResult = await _authService.RegisterAsync(userForRegisterDto, userForRegisterDto.Password);

            //AccessToken 
            var accessToken = await _authService.CreateAccessToken(registerResult);
            return Ok(accessToken);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {

            var userToLogin = await _authService.LoginAsync(userForLoginDto);
            if (!userToLogin.Status)
            {
                throw new UserNotFoundException("Kullanıcı bulunamadı!");
            }
            var accessToken = await _authService.CreateAccessToken(userToLogin);

            return Ok(accessToken);
        }

    }
}

