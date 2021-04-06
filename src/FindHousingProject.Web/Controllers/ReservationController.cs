using FindHousingProject.BLL.Interfaces;
using FindHousingProject.BLL.Models;
using FindHousingProject.Common.Constants;
using FindHousingProject.Common.Resources;
using FindHousingProject.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FindHousingProject.Web.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IHousingManager _housingManager;
        private readonly IUserManager _userManager;
        private readonly IReservationManager _reservationManager;

        public ReservationController(
            IHousingManager housingManager,
            IUserManager userManager, 
            IReservationManager reservationManager)
        {
            _housingManager = housingManager ?? throw new ArgumentNullException(nameof(housingManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _reservationManager = reservationManager ?? throw new ArgumentNullException(nameof(reservationManager));
        }

        [HttpGet]
        public async Task<IActionResult> Reservation()
        {
            var userId = await _userManager.GetUserIdByEmailAsync(User.Identity.Name);
            return View(_reservationManager.GetAllUserReservations(userId));
        }

        [HttpPost, ActionName("Reservation")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reservation(ReservationsViewModel reservationsViewModel)
        {
            var housing = await _housingManager.GetHousingAsync(reservationsViewModel.Id);
            if (ModelState.IsValid)
            {
                var userId = await _userManager.GetUserIdByEmailAsync(User.Identity.Name);
                int days;

                var reservationDto = new ReservationDto()
                {
                    CheckIn = reservationsViewModel.CheckIn,
                    CheckOut = reservationsViewModel.CheckOut,
                };

                if (reservationDto.CheckIn == reservationDto.CheckOut)
                {
                    days = 1;
                }
                else
                {
                    days = (reservationDto.CheckOut - reservationDto.CheckIn).Days;
                }

                var totalAmount = days * housing.PricePerDay;

                var status = await _reservationManager.ReservationAsync(
                    housing.Id,
                    userId,
                    totalAmount,
                    reservationDto.CheckIn,
                    reservationDto.CheckOut);

                return status switch
                {
                    StatusConstant.Booked => RedirectToAction("Reservation"),
                    _ => RedirectToAction("Details", "Housing", new { housingId = housing.Id, message = ErrorResource.DataNotBooked }),
                };
            }
            return RedirectToAction("Details", "Housing", new { housingId = housing.Id, message = ErrorResource.EnterData });
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteReservation(string reservationId)
        {
            var userId = await _userManager.GetUserIdByEmailAsync(User.Identity.Name);

            await _reservationManager.DeleteReservationAsync(reservationId, userId);

            return RedirectToAction("Reservation");
        }
    }
}
