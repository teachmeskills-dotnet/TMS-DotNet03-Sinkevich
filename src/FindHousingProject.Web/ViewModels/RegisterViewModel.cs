﻿using System.ComponentModel.DataAnnotations;

namespace FindHousingProject.Web.ViewModels
{
    /// <summary>
    /// Register view model.
    /// </summary>
    public class RegisterViewModel
    {
        /// <summary>
        /// Email.
        /// </summary>
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// Password confirm.
        /// </summary>
        [Required]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string PasswordConfirm { get; set; }
    }
}
