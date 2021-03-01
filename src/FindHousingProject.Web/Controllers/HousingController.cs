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
using System.IO;

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
                if (сreateHousingViewModel.NewScenery != null)
                {
                    byte[] imageData = null;
                    using (var binaryReader = new BinaryReader(сreateHousingViewModel.NewScenery.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)сreateHousingViewModel.NewScenery.Length);
                    }
                    housingDto.Scenery= imageData;
                }
                await _ihousingManager.CreateAsync(housingDto);
                return RedirectToAction( "Index", "Housing");
            }
            return View(сreateHousingViewModel);
        }
        public async Task<IActionResult> Delete(string housingId)
        {
            var housing = await _ihousingManager.GetHousingAsync(housingId);

            var housingEditViewModel = new CreateHousingViewModel()
            {
                Id = housing.Id,
                Description = housing.Description,
                PricePerDay = housing.PricePerDay,
                Name = housing.Name
            };
            return View(housingEditViewModel);
        }
        // [Authorize(Roles = RolesConstants.OwnerRole)]
        [HttpPost,  ActionName("Delete") ]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteHousing(string housingId)
        {
            var userId = await _usManager.GetUserIdByEmailAsync(User.Identity.Name);

            await _ihousingManager.DeleteAsync(housingId, userId);

            return RedirectToAction("Index", "Housing");
        }

        public async Task<IActionResult> Edit(string housingId)
        {
            var housing = await _ihousingManager.GetHousingAsync(housingId);

            var housingEditViewModel = new CreateHousingViewModel()
            {
                Id = housing.Id,
                Description = housing.Description,
                PricePerDay = housing.PricePerDay,
                Name = housing.Name,
                Address=housing.Address,
                Scenery=housing.Scenery
            };
            return View(housingEditViewModel);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditHousing(CreateHousingViewModel createHousingViewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = await _usManager.GetUserIdByEmailAsync(User.Identity.Name);
                var housingDto = new HousingDto()
                {
                    Id = createHousingViewModel.Id,
                    Description = createHousingViewModel.Description,
                    PricePerDay =createHousingViewModel.PricePerDay,
                    Address=createHousingViewModel.Address,
                    Name=createHousingViewModel.Name,
                };
                if (createHousingViewModel.NewScenery != null)
                {
                    byte[] imageData = null;
                    using (var binaryReader = new BinaryReader(createHousingViewModel.NewScenery.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)createHousingViewModel.NewScenery.Length);
                    }
                    housingDto.Scenery = imageData;
                }
                await _ihousingManager.UpdateHousingAsync(housingDto, userId);
                return RedirectToAction("Index", "Housing");
            }
            return View(createHousingViewModel);
        }
    }
}