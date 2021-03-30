using System;

namespace FindHousingProject.BLL.Models
{
    /// <summary>
    /// Reservation data transfer object.
    /// </summary>
    public class ReservationDto
    {
        /// <summary>
        /// Date to check-in.
        /// </summary>
        public DateTime CheckIn { get; set; }

        /// <summary>
        /// Date to check-out.
        /// </summary>
        public DateTime CheckOut { get; set; }

        /// <summary>
        /// Status of your reservation. This functionality will be created later.
        /// </summary>
        public string Status { get; set; }
    }
}
