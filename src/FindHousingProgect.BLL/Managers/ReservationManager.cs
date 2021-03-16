using FindHousingProject.BLL.Interfaces;
using FindHousingProject.BLL.Models;
using FindHousingProject.DAL.Entities;
using FindHousingProject.Common.Constants;
using FindHousingProject.Common.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindHousingProject.BLL.Managers
{
    public class ReservationManager : IReservationManager
    {
        private readonly IRepository<Housing> _repositoryHousing;
        private readonly IUserManager _userManager;
        private readonly IRepository<User> _repositoryUser;
        private readonly IRepository<Place> _repositoryPlace;
        private readonly IRepository<Reservation> _repositoryReservation;
        public ReservationManager(IRepository<Housing> repositoryHousing, IUserManager userManager, IRepository<User> repositoryUser, IRepository<Place> repositoryPlace, IRepository<Reservation> repositoryReservation)
        {
            _repositoryHousing = repositoryHousing ?? throw new ArgumentNullException(nameof(repositoryHousing));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _repositoryUser = repositoryUser ?? throw new ArgumentNullException(nameof(repositoryUser));
            _repositoryPlace = repositoryPlace ?? throw new ArgumentNullException(nameof(repositoryPlace));
            _repositoryReservation = repositoryReservation ?? throw new ArgumentNullException(nameof(repositoryReservation));
        }
        public IEnumerable<Reservation> GetAllUserReservations(String userId)
        {
            return _repositoryReservation.GetAll().Where(x => x.UserId == userId).Include(x => x.Housing);
        }
        public async Task<String> ReservationAsync(String housingId, String userId, decimal amount, DateTime checkIn, DateTime checkOut)
        {
            if (checkIn == null || checkOut == null)
            {
                return StatusConstants.BookedError;
            }
            var housing = await _repositoryHousing.GetEntityAsync(x => x.Id == housingId);
            var reservationDtos = new List<Reservation>();
            var reservstionPeriod = Period.Create(checkIn, checkOut);
            var reservations = await _repositoryReservation
                .GetAll().AsNoTracking().ToListAsync();
            reservations = reservations.Where(x => Period.Create(x.CheckIn, x.CheckOut).IsIntersectOrInclude(reservstionPeriod)).ToList();

            if(reservations.Any())
            {
                return StatusConstants.BookedError;
            }

            var user = await _repositoryUser.GetEntityAsync(x => x.Id == userId);
            var reservation = new Reservation
            {
                HousingId = housing.Id,
                Housing = housing,
                User = user,
                UserId = userId,
                CheckIn = checkIn,
                CheckOut = checkOut,
                Amount = amount,
                State = StateConstants.requested
            };
            await _repositoryReservation.CreateAsync(reservation);
            await _repositoryReservation.SaveChangesAsync();
            return StatusConstants.Booked;
        }
    }
}
