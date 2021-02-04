using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FindHousingProject.DAL.Entities
{
    /// <summary>
    /// User information.
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        /// User Id.
        /// </summary>
       //  public int Id { get; set; }
        /// <summary>
        /// User full name.
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// User mail.
        /// </summary>
       // public string Email { get; set; }

        /// <summary>
        /// Avatar.
        /// </summary>
        public byte[] Avatar { get; set; }


        /// <summary>
        /// User telefon.
        /// </summary>
        //public ulong PhoneNumber { get; set; }
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
