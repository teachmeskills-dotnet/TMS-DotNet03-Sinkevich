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
    public class UsManager : IUserManager
    {
        private readonly UserManager<User> _userManager;
        private readonly IRepository<User> _repositoryUser;

        public UsManager(UserManager<User> userManager, IRepository<User> repositoryUser)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _repositoryUser = repositoryUser ?? throw new ArgumentNullException(nameof(repositoryUser));
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
            var profile = await _repositoryUser.GetEntityAsync(profile => profile.Id == id && profile.Email == email);
            if (profile is null)
            {
                throw new KeyNotFoundException(ErrorResource.UserNotFound);
            }

            _repositoryUser.Delete(profile);
            await _repositoryUser.SaveChangesAsync();
        }

        public async Task UpdateProfileAsync(UserDto userDto)
        {
            userDto = userDto ?? throw new ArgumentNullException(nameof(userDto));

            var userDAL = await _repositoryUser.GetEntityAsync(profile => profile.Email == userDto.Email);

            if (userDAL is null)
            {
                throw new KeyNotFoundException(ErrorResource.UserNotFound);
            }
            if (userDto.Avatar != null)
            {
                userDAL.Avatar = userDto.Avatar;
            }
            userDAL.FullName = userDto.FullName;
            userDAL.Role = userDto.Role;
            _repositoryUser.Update(userDAL);
            await _repositoryUser.SaveChangesAsync();
        }

        public async Task CreateAsync(UserDto userDto)
        {
            userDto = userDto ?? throw new ArgumentNullException(nameof(userDto));

            var profile = new User
            {
                Id = userDto.Id,
                Email = userDto.Email
            };

            await _repositoryUser.CreateAsync(profile);
            await _repositoryUser.SaveChangesAsync();
        }

        public async Task<UserDto> GetAsync(string email)
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
                Role = profile.Role,
                Avatar = profile.Avatar
            };
            return userDto;
        }
    }
}
