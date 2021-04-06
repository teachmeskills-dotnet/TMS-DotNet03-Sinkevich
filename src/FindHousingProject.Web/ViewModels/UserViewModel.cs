using FindHousingProject.Common.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FindHousingProject.Web.ViewModels
{
    /// <summary>
    /// User view model.
    /// </summary>
    public class UserViewModel
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// By default role is guest.
        /// </summary>
        public string Role { get; set; }

        public List<SelectListItem> Roles
        {
            get => new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = RoleConstant.Owner,
                    Text= "Owner"
                },
                new SelectListItem
                {
                    Value = RoleConstant.Guest,
                    Text= "Guest"
                }
            };
        }

        /// <summary>
        /// Is owner.
        /// </summary>
        public bool IsOwner { get; set; }

        /// <summary>
        /// Pretty full name.
        /// </summary>
        public string PrettyFullName { get => FullName ?? "Enter full name"; }

        /// <summary>
        /// Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Full Name.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Avatar.
        /// </summary>
        public byte[] Avatar { get; set; }

        /// <summary>
        /// New avatar.
        /// </summary>
        [Display(Name = "Change avatar")]
        public IFormFile NewAvatar { get; set; }
    }
}