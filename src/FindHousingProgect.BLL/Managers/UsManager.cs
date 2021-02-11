using FindHousingProject.BLL.Interfaces;
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
                await CreateAsync(new UserDto
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
        public async Task UpdateProfileAsync(UserDto userDto, string email)
        {
            userDto = userDto ?? throw new ArgumentNullException(nameof(userDto));

            var userDAL = await _repositoryUser.GetEntityAsync(profile => profile.Email == userDto.Email);

            if (userDAL is null)
            {
                throw new KeyNotFoundException(ErrorResource.UserNotFound);
            }

            static bool ValidateToUpdate(User userDAL, UserDto userDto)
            {
                bool updated = false;

                if (userDAL.FullName != userDto.FullName)
                {
                    userDAL.FullName = userDto.FullName;
                    updated = true;
                }

                if (userDAL.Avatar != userDto.Avatar && userDto.Avatar != null)
                {
                    userDAL.Avatar = userDto.Avatar;
                    updated = true;
                }
                return updated;
            }

            var result = ValidateToUpdate(userDAL, userDto);
            if (result)
            {
                await _repositoryUser.SaveChangesAsync();
            }

            /*if (userDAL.IsOwner != userBLL.IsOwner)
            {
                await SwitchProfileStatusAsync(email);
            }*/
        }
        public async Task CreateAsync(UserDto userDto)
        {
            userDto = userDto ?? throw new ArgumentNullException(nameof(userDto));

            var profile = new User
            {
                Id = userDto.Id,
               // IsOwner = userBLL.IsOwner,
                Email = userDto.Email
            };

            await _repositoryUser.CreateAsync(profile);
            await _repositoryUser.SaveChangesAsync();
        }

        public async Task<UserDto> GetProfileAsync(string email)
        {
            var profile = await _repositoryUser.GetEntityAsync(profile => profile.Email == email);
            if (profile is null)
            {
                throw new KeyNotFoundException(ErrorResource.ProfileNotFound);
            }

            var userDto = new UserDto
            {
                Id = profile.Id,
                Email = profile.Email,
                FullName = profile.FullName,
               // IsOwner = profile.IsOwner,
                Avatar = profile.Avatar
            };
            return userDto;
        }
    }
}
