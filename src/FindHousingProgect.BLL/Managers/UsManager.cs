﻿using FindHousingProject.BLL.Interfaces;
using FindHousingProject.BLL.Models;
using FindHousingProject.Common.Constants;
using FindHousingProject.Common.Resources;
using FindHousingProject.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FindHousingProject.BLL.Managers
{
    ///<inheritdoc cref="IUserManager"/>

    public class UsManager: IUserManager
    {
        private readonly UserManager<User> _userManager;
      //  private readonly IUserManager _iuserManager;
        private readonly IRepository<User> _repositoryUser;
       /* public UserManager( UserManager<User> userManager, IUserManager iuserManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
           // _iuserManager = iuserManager ?? throw new ArgumentNullException(nameof(iuserManager));
        }*/
       public UsManager(UserManager<User> userManager, IRepository<User> repositoryUser)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _repositoryUser = repositoryUser ?? throw new ArgumentNullException(nameof(repositoryUser));
           // _iuserManager = iuserManager ?? throw new ArgumentNullException(nameof(iuserManager));
        }
        public async Task<(IdentityResult, User)> SignUpAsync(
                    string email,
                    string password,
                    bool isOwner)
        {
            User userEmail = new User()
            {
                Email = email,
            };

            var result = await _userManager.CreateAsync(userEmail, password);
            if (result.Succeeded)
            {
                if (isOwner)
                {
                    await _userManager.AddToRoleAsync(userEmail, RolesConstants.OwnerRole);
                }
                await CreateAsync(new UserBLL
                {
                    IsOwner = isOwner,
                    Email = userEmail.Email
                });
            }
            return (result, userEmail);
        }
        public async Task<string> GetUserIdByEmailAsync(string email)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(user => user.UserName == email);
            if (user is null)
            {
                throw new KeyNotFoundException(ErrorResource.UserNotFound);
            }
            return user.Id;
        }
        public async Task<IdentityResult> ChangePasswordAsync(string email, string oldPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(email);

            if (user is null)
            {
                throw new KeyNotFoundException(ErrorResource.UserNotFound);
            }

            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            return result;
        }
        public async Task DeleteAsync(string id, string email)
        {
            var profile = await _repositoryUser.GetEntityAsync(profile => profile.Id== id && profile.Email == email);
            if (profile is null)
            {
                throw new KeyNotFoundException(ErrorResource.UserNotFound);
            }

            _repositoryUser.Delete(profile);
            await _repositoryUser.SaveChangesAsync();
        }
        public async Task UpdateProfileAsync(UserBLL userBLL, string email)
        {
            userBLL = userBLL ?? throw new ArgumentNullException(nameof(userBLL));

            var userDAL = await _repositoryUser.GetEntityAsync(profile => profile.Email == userBLL.Email);

            if (userDAL is null)
            {
                throw new KeyNotFoundException(ErrorResource.UserNotFound);
            }

            static bool ValidateToUpdate(User userDAL, UserBLL userBLL)
            {
                bool updated = false;

                if (userDAL.FullName != userBLL.FullName)
                {
                    userDAL.FullName = userBLL.FullName;
                    updated = true;
                }

                if (userDAL.Avatar != userBLL.Avatar && userBLL.Avatar != null)
                {
                    userDAL.Avatar = userBLL.Avatar;
                    updated = true;
                }
                return updated;
            }

            var result = ValidateToUpdate(userDAL, userBLL);
            if (result)
            {
                await _repositoryUser.SaveChangesAsync();
            }

            /*if (userDAL.IsOwner != userBLL.IsOwner)
            {
                await SwitchProfileStatusAsync(email);
            }*/
        }
        public async Task CreateAsync(UserBLL userBLL)
        {
            userBLL = userBLL ?? throw new ArgumentNullException(nameof(userBLL));

            var profile = new User
            {
                Id = userBLL.Id,
               // IsOwner = userBLL.IsOwner,
                Email = userBLL.Email
            };

            await _repositoryUser.CreateAsync(profile);
            await _repositoryUser.SaveChangesAsync();
        }

        public async Task<UserBLL> GetProfileAsync(string email)
        {
            var profile = await _repositoryUser.GetEntityAsync(profile => profile.Email == email);
            if (profile is null)
            {
                throw new KeyNotFoundException(ErrorResource.ProfileNotFound);
            }

            var userBll = new UserBLL
            {
                Id = profile.Id,
                Email = profile.Email,
                FullName = profile.FullName,
               // IsOwner = profile.IsOwner,
                Avatar = profile.Avatar
            };
            return userBll;
        }
    }
}
