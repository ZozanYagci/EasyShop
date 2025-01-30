using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {

        private readonly IUserDal _userDal;
        private readonly ITokenHelper _tokenHelper;

        public UserManager(IUserDal userDal, ITokenHelper tokenHelper)
        {
            _userDal = userDal;
            _tokenHelper = tokenHelper;
        }
        public Task AddAsync(AuthUser entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<AuthUser>> GetAllAsync(bool noTracking = true)
        {
            throw new NotImplementedException();
        }

        public Task<AuthUser> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<AccessToken> LoginAsync(string email, string password)
        {
            var authUser = await _userDal.GetByEmailAsync(email);
            if (authUser == null || !PasswordHasher.VerifyPassword(authUser.PasswordHash, password))
            {
                throw new UnauthorizedAccessException("Kullanıcı adı veya şifre hatalı");
            }

            var operationClaims = authUser.AuthUserOperationClaims.Select(uoc=>uoc.OperationClaim).ToList();
            var token = _tokenHelper.CreateToken(authUser, operationClaims);
            return token;
        }

        public async Task<string> RegisterAsync(AuthUser user, string password)
        {
            var existingUser = await _userDal.GetByEmailAsync(user.Email);
            if (existingUser != null)
            {
                throw new Exception("Kullanıcı zaten mevcut");
            }
            user.PasswordHash = PasswordHasher.HashPassword(password);
            await _userDal.AddAsync(user);
            return "Kullanıcı başarıyla kaydoldu.";
        }

        public Task UpdateAsync(AuthUser entity)
        {
            throw new NotImplementedException();
        }

       
    }
}
