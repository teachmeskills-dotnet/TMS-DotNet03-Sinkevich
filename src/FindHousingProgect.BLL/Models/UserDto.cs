using System;
using System.Collections.Generic;
using System.Text;

namespace FindHousingProject.BLL.Models
{
    public class UserDto
    {
        /// <summary>
        /// Identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Full Name.
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Role .
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Avatar.
        /// </summary>
        public byte[] Avatar { get; set; }

        /// <summary>
        /// Is vendor.
        /// </summary>
        public bool IsOwner { get; set; }
    }
}
