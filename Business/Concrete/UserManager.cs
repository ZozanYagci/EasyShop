using Business.Abstract;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
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

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public async Task<int> AddAsync(AuthUser authUser)
        {
            return await _userDal.AddAsync(authUser);
        }

        //public void Add(AuthUser authUser)
        //{
        //   _userDal.Add(authUser);
        //}

        public async Task<AuthUser> GetByEmailAsync(string email)
        {
            
            return await _userDal.Get(x => x.Email == email);
        }

        public async Task<List<OperationClaim>> GetClaims(AuthUser user)
        {
            return await _userDal.GetClaim(user);
        }
    }
}
