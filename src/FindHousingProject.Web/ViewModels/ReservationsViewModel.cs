using System;

namespace FindHousingProject.Web.ViewModels
{
    /// <summary>
    /// Reservations view model.
    /// </summary>
    public class ReservationsViewModel
    {
        /// <summary>
        /// Reservation id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Reservation check-in.
        /// </summary>
        public DateTime CheckIn { get; set; }

        /// <summary>
        /// Reservation check-out.
        /// </summary>
        public DateTime CheckOut { get; set; }
    }
}
