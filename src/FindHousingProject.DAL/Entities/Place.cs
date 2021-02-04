using System;
using System.Collections.Generic;
using System.Text;

namespace FindHousingProject.DAL.Entities
{
    /// <summary>
    /// User about the place (resort, town, village, island and so on).
    /// </summary>
    public class Place
    {
        /// <summary>
        /// Place Id.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// User Country Id.
        /// </summary>
        public string CountryId { get; set; }
        /// <summary>
        /// User Country.
        /// </summary>
        public Country Country { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public  ICollection<Housing> Housings { get; set; }
        /// <summary>
        /// Type of housing (hotel, house, apartment, hostel, etc.).
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Type name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Location information.
        /// </summary>
        public string Description { get; set; }
    }
}
