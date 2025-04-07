using Core.Entities.Concrete;
using DTOs.DTOs.UserDtos;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        Task<List<OperationClaim>> GetClaims(AuthUser user);
        Task<AuthUser> GetByEmailAsync(string email);
        //void Add(AuthUser authUser);
        Task<int> AddAsync(AuthUser authUser);
        Task<int> UpdateUserAsync(int userId, UserProfileUpdateDto userUpdate);   

        Task<UserProfileUpdateDto> GetByIdAsync(int id)
    }
}
