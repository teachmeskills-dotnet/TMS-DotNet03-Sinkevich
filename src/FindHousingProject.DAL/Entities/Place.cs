using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FindHousingProject.DAL.Entities
{
    /// <summary>
    /// User(owner) describes the place.
    /// </summary>
    public class Place
    {
        /// <summary>
        /// Place Id.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        /// <summary>
        /// User's country Id.
        /// </summary>
        public string CountryId { get; set; }

        /// <summary>
        /// User's country.
        /// </summary>
        public Country Country { get; set; }

        /// <summary>
        /// Housing.
        /// </summary>
        public ICollection<Housing> Housings { get; set; }

        /// <summary>
        /// Type of housing (hotel, house, apartment, hostel, etc.).
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Type name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Housing information.
        /// </summary>
        public string Description { get; set; }
    }
}
