using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace FindHousingProject.DAL.Entities
{
    /// <summary>
    /// User information.
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        /// User full name.
        /// </summary>
        public string? FullName { get; set; }

        /// <summary>
        /// User role name.
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Avatar.
        /// </summary>
        public byte[] Avatar { get; set; }

        /// <summary>
        /// User date of Birth.
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// User identity document.
        /// </summary>
        public string Documents{ get; set; }

        /// <summary>
        /// User gender
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        ///
        /// </summary>
        public ICollection<Reservation> Reservations { get; set; }

        /// <summary>
        ///
        /// </summary>
        public ICollection<Housing> Housings { get; set; }
    }
}
