using Business.Abstract;
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
        private readonly JwtHelper _jwtHelper;

        public UserManager(IUserDal userDal, JwtHelper jwtHelper)
        {
            _userDal = userDal;
            _jwtHelper = jwtHelper;
        }
        public Task AddAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllAsync(bool noTracking = true)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await _userDal.GetByEmailAsync(email);
            if (user == null || !PasswordHasher.VerifyPassword(user.PasswordHash, password))
            {
                throw new Exception("Kullanıcı adı veya şifre hatalı");
            }

            var token = _jwtHelper.GenerateToken(user.Id, user.Email);
            return token;
        }

        public async Task<string> RegisterAsync(User user, string password)
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

        public Task UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
