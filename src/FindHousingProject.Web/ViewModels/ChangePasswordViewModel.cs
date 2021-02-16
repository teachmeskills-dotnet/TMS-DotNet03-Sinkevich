using System.ComponentModel.DataAnnotations;

namespace FindHousingProject.Web.ViewModels
{
    /// <summary>
    /// Change password view model.
    /// </summary>
    public class ChangePasswordViewModel
    {
        /// <summary>
        /// Old password.
        /// </summary>
        [Required(ErrorMessage = "Enter your old password")]
        [DataType(DataType.Password)]
        [Display(Name = "Old password")]
        public string OldPassword { get; set; }

        /// <summary>
        /// New password.
        /// </summary>
        [Required(ErrorMessage = "Enter your new password")]
        // [MinLength(8, ErrorMessage = "Пароль слишком короткий (минимум 8 символов)")]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        /// <summary>
        /// New password confirm.
        /// </summary>
        [Required]
        [Compare("NewPassword", ErrorMessage = "Passwords don't match")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string NewPasswordConfirm { get; set; }
    }
}