﻿using FindHousingProject.BLL.Interfaces;
using FindHousingProject.BLL.Models;
using FindHousingProject.Common.Constants;
using FindHousingProject.Common.Resources;
using FindHousingProject.Common.Utils;
using FindHousingProject.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindHousingProject.BLL.Managers
{
    /// <inheritdoc cref="IReservationManager"/>
    public class ReservationManager : IReservationManager
    {
        private readonly IRepository<Housing> _repositoryHousing;
        private readonly IRepository<User> _repositoryUser;
        private readonly IRepository<Reservation> _repositoryReservation;

        public ReservationManager(IRepository<Housing> repositoryHousing, IRepository<User> repositoryUser, IRepository<Reservation> repositoryReservation)
        {
            _repositoryHousing = repositoryHousing ?? throw new ArgumentNullException(nameof(repositoryHousing));
            _repositoryUser = repositoryUser ?? throw new ArgumentNullException(nameof(repositoryUser));
            _repositoryReservation = repositoryReservation ?? throw new ArgumentNullException(nameof(repositoryReservation));
        }

        public IEnumerable<Reservation> GetAllUserReservations(String userId)
        {
            return _repositoryReservation
                .GetAll()
                .Where(x => x.UserId == userId)
                .Include(x => x.Housing);
        }

        public async Task<String> ReservationAsync(String housingId, String userId, decimal amount, DateTime checkIn, DateTime checkOut)
        {
            if (checkIn == null || checkOut == null)
            {
                return StatusConstant.BookedError;
            }

            var housing = await _repositoryHousing.GetEntityAsync(x => x.Id == housingId);
            var reservationDtos = new List<Reservation>();
            var reservstionPeriod = Period.Create(checkIn, checkOut);
            var reservations = await _repositoryReservation.GetAll().AsNoTracking().ToListAsync();

            reservations = reservations.Where(x => Period.Create(x.CheckIn, x.CheckOut).IsIntersectOrInclude(reservstionPeriod)).ToList();
            reservations.RemoveAll(x => x.HousingId != housingId);

            if (reservations.Any())
            {
                return StatusConstant.BookedError;
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
                State = StateConstant.requested
            };

            await _repositoryReservation.CreateAsync(reservation);
            await _repositoryReservation.SaveChangesAsync();

            return StatusConstant.Booked;
        }

        public async Task DeleteReservationAsync(string id, string userId)
        {
            var reservation = await _repositoryReservation.GetEntityAsync(reservation => reservation.Id == id && reservation.UserId == userId);

            if (reservation is null)
            {
                throw new KeyNotFoundException(ErrorResource.ReservationNotFound);
            }

            _repositoryReservation.Delete(reservation);
            await _repositoryReservation.SaveChangesAsync();
        }

        public async Task<ReservationDto> GetReservationAsync(string id)
        {
            var reservation = await _repositoryReservation.GetAll().FirstOrDefaultAsync(r => r.Id == id);

            if (reservation is null)
            {
                throw new KeyNotFoundException(ErrorResource.ReservationNotFound);
            }

            var reservationDto = new ReservationDto
            {
                CheckIn = reservation.CheckIn,
                CheckOut = reservation.CheckOut
            };

            return reservationDto;
        }
    }
}
