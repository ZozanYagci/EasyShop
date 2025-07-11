﻿using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Exceptions;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using DTOs.DTOs.UserDtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
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
        private readonly IMapper _mapper;

        public UserManager(IUserDal userDal, IMapper mapper)
        {
            _userDal = userDal;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(AuthUser authUser)
        {
            return await _userDal.AddAsync(authUser);
        }

        public async Task<IResult> ChangePasswordAsync(int userId, ChangePasswordDto changePassword)
        {
            var user = await _userDal.Get(u => u.Id == userId);
            if (user == null)
                return new ErrorResult(AuthMessages.UserNotFound);

            var isOldPasswordCorrect = HashingHelper.VerifyPasswordHash(changePassword.OldPassword, user.PasswordHash, user.PasswordSalt);

            if (!isOldPasswordCorrect)
                return new ErrorResult(AuthMessages.PasswordError);

            HashingHelper.CreatePasswordHash(changePassword.NewPassword, out byte[] newHash, out byte[] newSalt);
            user.PasswordHash = newHash;
            user.PasswordSalt = newSalt;

            await _userDal.UpdateAsync(user);
            return new SuccessResult(AuthMessages.PasswordChanged);

        }

        //public void Add(AuthUser authUser)
        //{
        //   _userDal.Add(authUser);
        //}

        public async Task<AuthUser> GetByEmailAsync(string email)
        {

            return await _userDal.Get(x => x.Email == email);
        }

        public async Task<UserProfileUpdateDto> GetByIdAsync(int id)
        {
            var user = await _userDal.Get(u => u.Id == id);
            if (user == null)
                throw new UserNotFoundException(Messages.UserNotFound);

            return _mapper.Map<UserProfileUpdateDto>(user);
        }

        public async Task<List<OperationClaim>> GetClaims(AuthUser user)
        {
            return await _userDal.GetClaim(user);
        }

        public async Task<int> UpdateUserAsync(int userId, UserProfileUpdateDto userUpdate)
        {
            var user = await _userDal.Get(u => u.Id == userId);
            if (user == null)
                throw new UserNotFoundException(Messages.UserNotFound);

            _mapper.Map(userUpdate, user);


            var existingUser = await _userDal.Get(u => u.Email.ToLower() == userUpdate.Email.ToLower() && u.Id != userId);
            if (existingUser != null)
            {
                throw new Exception("E-posta zaten kullanılıyor");
            }
            return await _userDal.UpdateAsync(user);
        }
    }
}
