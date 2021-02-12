using System;
using System.Collections.Generic;
using System.Text;

namespace FindHousingProject.DAL.Entities
{
    /// <summary>
    /// Housing information.
    /// </summary>
    public class Housing
    {
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
        /// User which booking housing
        /// </summary>
        public User User { get; set; }

        /// <summary>
        ///
        /// </summary>
        public ICollection<Reservation> Reservations { get; set; }

        /// <summary>
        /// Housing name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Cost per day.
        /// </summary>
        public decimal PricePerDay { get; set; }
        /// <summary>
        /// Number of all seats.
        /// </summary>
        public int NumberOfSeats { get; set; }
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
        /// .
        /// </summary>
        public byte[] Scenery { get; set; }
    }
}
