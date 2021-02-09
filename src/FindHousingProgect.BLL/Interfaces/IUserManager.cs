using FindHousingProject.BLL.Models;
using FindHousingProject.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace FindHousingProject.BLL.Interfaces
{
    /// <summary>
    /// User manager.
    /// </summary>
    public interface IUserManager
    {
        /// <summary>
        /// Register or Sign up.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="password">Password.</param>
        /// <param name="isOwner">Is Owner.</param>
        /// <returns>Identity result and user.</returns>
        Task<(IdentityResult, User)> SignUpAsync(string email, string password, bool isOwner);

        /// <summary>
        /// Change password.
        /// </summary>
        /// <param name="email">User identifier.</param>
        /// <returns>Identity result.</returns>
        Task<IdentityResult> ChangePasswordAsync(string email, string oldPassword, string newPassword);

        /// <summary>
        /// Get user identifier by email.
        /// </summary>
        /// <param name="email">User email.</param>
        /// <returns>User identifier (GUID).</returns>
        Task<string> GetUserIdByEmailAsync(string email);

        /// <summary>
        /// Create profile.
        /// </summary>
        /// <param name="userBLL">Profile data transfer object.</param>
        Task CreateAsync(UserBLL userBLL);

        /// <summary>
        /// Delete profile by identifier.
        /// </summary>
        /// <param name="id">Identifier.</param>
        /// <param name="email">User identifier.</param>
        Task DeleteAsync(string id, string email);

        /// <summary>
        /// Update profile by identifier.
        /// </summary>
        /// <param name="userBLL">Profile data transfer object.</param>
        /// <param name="email">User identifier.</param>
        Task UpdateProfileAsync(UserBLL userBLL, string email);
    }
}
