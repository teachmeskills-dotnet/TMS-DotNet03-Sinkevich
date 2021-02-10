using System;
using System.Collections.Generic;

namespace FindHousingProject.Web.ViewModels
{
    /// <summary>
    /// Profile view model.
    /// </summary>
    public class UserViewModel
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Is owner.
        /// </summary>
        public bool IsOwner { get; set; }

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
    }
}