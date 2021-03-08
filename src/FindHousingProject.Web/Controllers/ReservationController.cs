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
        [HttpGet]

        public IActionResult Reservation()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Reservation(ReservationsViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Reservation", "Reservation");
            }
            return View(model);
        }
    }
}
