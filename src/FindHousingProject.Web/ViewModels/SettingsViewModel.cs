using FindHousingProject.Common.Constants;
using Microsoft.AspNetCore.Http;
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
        [Display(Name = "Owner?")]
        public bool IsOwner { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Enter full name")]
        [MaxLength(ConfigurationConstants.StandartLenghtForStringField)]
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