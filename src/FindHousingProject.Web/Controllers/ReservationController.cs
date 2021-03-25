using FindHousingProject.BLL.Interfaces;
using FindHousingProject.BLL.Models;
using FindHousingProject.Common.Resources;
using FindHousingProject.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using FindHousingProject.Common.Constants;
using System.Threading.Tasks;
using IronPdf;
using System;


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
                var reservationDto = new ReservationDto()
                {
                    CheckIn = reservationsViewModel.CheckIn,
                    CheckOut = reservationsViewModel.CheckOut,
                };
                var days = (reservationDto.CheckOut - reservationDto.CheckIn).Days;
                var totalAmount = days * housing.PricePerDay;
                var status = await _reservationManager.ReservationAsync(housing.Id, userId, totalAmount, reservationDto.CheckIn, reservationDto.CheckOut);
                switch (status)
                {
                    case StatusConstants.Booked:
                        return RedirectToAction("Reservation");
                    default:
                        return RedirectToAction("Details", "Housing", new { housingId = housing.Id, message = ErrorResource.DataNotBooked });
                        // break;
                }
            }
            return RedirectToAction("Details", "Housing", new { housingId = housing.Id, message = ErrorResource.EnterData/*message = "Exception happened"*/ });
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteReservation(string reservationId)
        {
            var userId = await _userManager.GetUserIdByEmailAsync(User.Identity.Name);

            await _reservationManager.DeleteReservationAsync(reservationId, userId);

            return RedirectToAction("Reservation");
        }
        [ActionName("Download")]
        public ActionResult DownloadPDF()
        {
            var PDF = HtmlToPdf.StaticRenderUrlAsPdf(new Uri("https://localhost:5001/Reservation/Reservation"));
            return File(PDF.BinaryData, "application/pdf", "Reservation.Pdf");

        }
    }
}
