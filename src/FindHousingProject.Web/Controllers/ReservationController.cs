using FindHousingProject.BLL.Interfaces;
using FindHousingProject.BLL.Models;
using FindHousingProject.DAL.Entities;
using FindHousingProject.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindHousingProject.Web.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IHousingManager _housingManager;
        private readonly IUserManager _userManager;
        private readonly IReservationManager _reservationManager;

        public ReservationController(IHousingManager housingManager, IUserManager userManager, IReservationManager reservationManager)
        {
            _housingManager = housingManager;
            _userManager = userManager;
            _reservationManager = reservationManager;
        }
        [HttpGet]
        public IActionResult Reservation()
        {
            return View();
        }
        [HttpPost, ActionName("Reservation")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reservation(ReservationsViewModel reservationsViewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = await _userManager.GetUserIdByEmailAsync(User.Identity.Name);
                var housing = await _housingManager.GetHousingAsync(reservationsViewModel.Id);
                var reservationDto = new ReservationDto()
                {
                    CheckIn = reservationsViewModel.CheckIn,
                    CheckOut = reservationsViewModel.CheckOut,
                };
                var days = (reservationDto.CheckOut - reservationDto.CheckIn).Days;
                var totalAmount = days * housing.PricePerDay;
                await _reservationManager.ReservationAsync(housing.Id, userId, totalAmount, reservationDto.CheckIn, reservationDto.CheckOut);
                return RedirectToAction("Reservation", "Reservation");
            }
            return View(reservationsViewModel);
        }
        //String housingId, String userId, decimal amount, DateTime checkIn, DateTime checkOut
    }
}
