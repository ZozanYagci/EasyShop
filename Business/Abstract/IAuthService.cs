using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using DTOs.DTOs.UserDtos;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        Task<ServiceResponse<AccessToken>> RegisterAsync(UserForRegisterDto registerDto, string password);
        Task<ServiceResponse<AccessToken>> LoginAsync(UserForLoginDto loginDto);
        Task<bool> UserExists(string email);
        Task<AccessToken> CreateAccessToken(AuthUser user);
    }
}
