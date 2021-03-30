using FindHousingProject.DAL.Entities;
using System;
using System.Collections.Generic;

namespace FindHousingProject.BLL.Models
{
    /// <summary>
    /// Housing data transfer object.
    /// </summary>
    public class HousingDto
    {
        public HousingDto() { }

        /// <summary>
        /// Housing Id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Place Id.
        /// </summary>
        public string PlaceId { get; set; }

        /// <summary>
        /// Place where you stay.
        /// </summary>
        public Place Place { get; set; }

        /// <summary>
        /// User Id.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// User who is booking housing
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Guest's reservations
        /// </summary>
        //public ICollection<Reservation> Reservations { get; set; }

        /// <summary>
        /// Housing name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The cost per day.
        /// </summary>
        public decimal PricePerDay { get; set; }

        /// <summary>
        /// Unfree days for booking.
        /// </summary>
        public DateTime? BookedFrom { get; set; }

        /// <summary>
        /// Free days for booking.
        /// </summary>
        public DateTime? BookedTo { get; set; }

        /// <summary>
        /// Discription about housing.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Housing address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Photo of housing.
        /// </summary>
        public byte[] Scenery { get; set; }
    }
}
