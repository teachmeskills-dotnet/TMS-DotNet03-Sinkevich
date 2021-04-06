using FindHousingProject.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FindHousingProject.BLL.Interfaces
{
    /// <summary>
    /// Reservation manager.
    /// </summary>
    public interface IReservationManager
    {
        /// <summary>
        /// Retrieving all lists of user's (guest's) reservation.
        /// <summary>
        /// <param name="userId">User email.</param>
        IEnumerable<Reservation> GetAllUserReservations(String userId);

        /// <summary>
        /// Making a reservation.
        /// <summary>
        Task<String> ReservationAsync(String housingId, String userId, decimal amount, DateTime checkIn, DateTime checkOut);

        /// <summary>
        /// Deleting a reservation by user.
        /// <summary>
        Task DeleteReservationAsync(string id, string userId);
    }
}
