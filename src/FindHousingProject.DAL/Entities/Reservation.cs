using System;
using System.Collections.Generic;
using System.Text;

namespace FindHousingProject.DAL.Entities
{
    /// <summary>
    /// Accommondation booking information.
    /// </summary>
    public class Reservation
    {
        /// <summary>
        /// Reservation Id.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Housing Id.
        /// </summary>
        public string HousingId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Housing Housing { get; set; }


        /// <summary>
        /// User Id.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public User User { get; set; }


        /// <summary>
        /// Arrival Date.
        /// </summary>
        public DateTime CheckIn { get; set; }
        /// <summary>
        /// Date of departure.
        /// </summary>
        public DateTime CheckOut { get; set; }
        /// <summary>
        /// Cost all days.
        /// </summary>
        public decimal Amount { get; set; }

        public byte State { get; set; }
    }
}
