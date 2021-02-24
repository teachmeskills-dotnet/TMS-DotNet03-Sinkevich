using FindHousingProject.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindHousingProject.BLL.Models;
using FindHousingProject.DAL.Entities;
using FindHousingProject.BLL.Managers;
using FindHousingProject.Web.ViewModels;
using FindHousingProject.Common.Constants;
using FindHousingProject.BLL.Interfaces;

namespace FindHousingProject.Web.Controllers
{
    public class HousingController : Controller
    {
        private readonly IHousingManager _ihousingManager;
        private readonly IUserManager _usManager;

        public HousingController( IHousingManager housingManager, IUserManager usManager)
        {
            _ihousingManager = housingManager;
            _usManager = usManager;
        }
        // GET: Housing
        public async Task<IActionResult> Index()
        {
            if (User.Identity.Name == null)
            {
                return RedirectToAction(  "SignIn", "Account");
            }
            else
            {
                return View(await _ihousingManager.GetCurrentHousingsAsync(User.Identity.Name));
            }
        }
        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateHousingViewModel сreateHousingViewModel)
        {
            if (ModelState.IsValid)
            {
                // var user = await _usManager.GetAsync(User.Identity.Name);
                var userId = await _usManager.GetUserIdByEmailAsync(User.Identity.Name);
                var housingDto = new HousingDto()
                {
                    UserId= userId,
                    Name = сreateHousingViewModel.Name,
                    PricePerDay = сreateHousingViewModel.PricePerDay,
                    Description = сreateHousingViewModel.Description,
                    Address = сreateHousingViewModel.Address,
                    Scenery = сreateHousingViewModel.Scenery
                };
                await _ihousingManager.CreateAsync(housingDto);
                return RedirectToAction( "Index", "Housing");
            }
            return View(сreateHousingViewModel);
        }
    }
}