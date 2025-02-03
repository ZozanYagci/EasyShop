using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Exceptions;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DTOs.DTOs.UserDtos;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {

        private readonly IUserService _userService;
        private readonly ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public async Task<AccessToken> CreateAccessToken(AuthUser user)
        {
            var claims = await _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return accessToken;
        }

        public async Task<AuthUser> LoginAsync(UserForLoginDto loginDto)
        {
            var userToCheck = await _userService.GetByEmailAsync(loginDto.Email)
                ?? throw new UserNotFoundException(Messages.UserNotFound);

            if (!HashingHelper.VerifyPasswordHash(loginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                throw new InvalidPasswordException(Messages.PasswordError);
            }

            return userToCheck;
        }

        public async Task<AuthUser> RegisterAsync(UserForRegisterDto registerDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new AuthUser
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };


            var existingUser = await _userService.GetByEmailAsync(user.Email);
            if (existingUser != null)
            {
                throw new UserAlreadyExistsException(Messages.UserAlreadyExists);
            }

            await _userService.AddAsync(user);
            return user;
        }

        public async Task<bool> UserExists(string email)
        {
            var user = await _userService.GetByEmailAsync(email);
            return user != null;
        }
    }
}
