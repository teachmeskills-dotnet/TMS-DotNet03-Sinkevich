using FindHousingProject.BLL.Models;
using FindHousingProject.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FindHousingProject.BLL.Interfaces
{
    public interface IReservationManager
    {
        IEnumerable<Reservation> GetAllUserReservations(String userId);
        Task<String> ReservationAsync(String housingId, String userId, decimal amount, DateTime checkIn, DateTime checkOut);
    }
}
