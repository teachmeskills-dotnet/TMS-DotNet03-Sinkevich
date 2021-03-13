using FindHousingProject.Common.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FindHousingProject.Web.ViewModels
{
    /// <summary>
    /// Settings view model.
    /// </summary>
    public class SettingsViewModel
    {
        /// <summary>
        /// Profile identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Is Owner.
        /// </summary>
        [Display(Name = "Choose a role:")]
        public string Role { get; set; }

        /// <summary>
        /// Is Owner.
        /// </summary>
        [Display(Name = "Owner?")]
        public bool IsOwner { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Enter full name")]
        [MaxLength(ConfigurationConstants.StandartLenghtForStringField)] //https://docs.fluentvalidation.net/en/latest/aspnet.html
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
        //public List<SelectListItem> Roles { get; set; }
        public List<SelectListItem> Roles
        {
            get => new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> {new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem {
                    Value = RolesConstants.OwnerRole, Text= "Owner"},new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem{ Value = RolesConstants.GuestRole, Text= "Guest"} };
        }
    }
}