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
                return RedirectToAction("SignIn", "Account");
            }
            else
            {
                var user = await _usManager.GetAsync(User.Identity.Name);
                if(user?.Role != RolesConstants.OwnerRole)
                {
                    return RedirectToAction("Another", "Home");
                }
                else
                {
                    return View(await _ihousingManager.GetCurrentHousingsAsync(User.Identity.Name));
                }
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(HousingViewModel housingViewModel)
        {
            if (ModelState.IsValid)
            {
                // var user = await _usManager.GetAsync(User.Identity.Name);
                var userId = await _usManager.GetUserIdByEmailAsync(User.Identity.Name);
                var placeDto = new Place()
                {
                    Name = housingViewModel.Place,

                };
                var housingDto = new HousingDto()
                {
                    UserId= userId,
                    Name = housingViewModel.Name,
                    Place = placeDto,
                    PricePerDay = housingViewModel.PricePerDay,
                    Description = housingViewModel.Description,
                    Address = housingViewModel.Address,
                    Scenery = housingViewModel.Scenery
                };
                if (housingViewModel.NewScenery != null)
                {
                    byte[] imageData = null;
                    using (var binaryReader = new BinaryReader(housingViewModel.NewScenery.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)housingViewModel.NewScenery.Length);
                    }
                    housingDto.Scenery= imageData;
                }
                await _ihousingManager.CreateAsync(housingDto);
                return RedirectToAction( "Index", "Housing");
            }
            return View(housingViewModel);
        }

        public async Task<IActionResult> Delete(string housingId)
        {
            var housing = await _ihousingManager.GetHousingAsync(housingId);

            var housingEditViewModel = new HousingViewModel()
            {
                Id = housing.Id,
                Place=housing.Place?.Name,
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

            var housingEditViewModel = new HousingViewModel()
            {
                Id = housing.Id,
                Place = housing.Place?.Name,
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
        public async Task<IActionResult> EditHousing(HousingViewModel housingViewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = await _usManager.GetUserIdByEmailAsync(User.Identity.Name);
                var placeDto = new Place()
                {
                    Name = housingViewModel.Place,

                };
                var housingDto = new HousingDto()
                {
                    Id = housingViewModel.Id,
                    Place = placeDto,
                    Description = housingViewModel.Description,
                    PricePerDay =housingViewModel.PricePerDay,
                    Address=housingViewModel.Address,
                    Name=housingViewModel.Name,
                    Scenery=housingViewModel.Scenery
                };
                if (housingViewModel.NewScenery != null)
                {
                    byte[] imageData = null;
                    using (var binaryReader = new BinaryReader(housingViewModel.NewScenery.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)housingViewModel.NewScenery.Length);
                    }
                    housingDto.Scenery = imageData;
                }
                await _ihousingManager.UpdateHousingAsync(housingDto, userId);
                return RedirectToAction("Index", "Housing");
            }
            return View(housingViewModel);
        }

        public async Task<IActionResult> Details(string housingId, string message = null)
        {
            if (User.Identity.Name == null)
            {
                return RedirectToAction("SignIn", "Account");
            }
            else
            {
                var housing = await _ihousingManager.GetHousingAsync(housingId);
                var user = await _usManager.GetAsync(User.Identity.Name);

                if(message != null)
                {
                    ModelState.AddModelError("Error", message);
                }

                var housingDetailsViewModel = new HousingViewModel()
                {
                    Id = housing.Id,
                    User = user,
                    Name = housing.Name,
                    Place = housing.Place?.Name,
                    Address = housing.Address,
                    PricePerDay = housing.PricePerDay,
                    Description = housing.Description,
                    Scenery = housing.Scenery
                };

                return View(housingDetailsViewModel);
            }
        }

        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DetailsHousing(HousingViewModel housingViewModel)
        {
            var placeDto = new Place()
            {
                Name = housingViewModel.Place,
            };

            if (ModelState.IsValid)
            {
                var userId = await _usManager.GetUserIdByEmailAsync(User.Identity.Name);

                var housingDto = new HousingDto()
                {
                    Id = housingViewModel.Id,
                    Place = placeDto,
                    Description = housingViewModel.Description,
                    PricePerDay = housingViewModel.PricePerDay,
                    Address = housingViewModel.Address,
                    Name = housingViewModel.Name,
                };
                return RedirectToAction("Index", "Housing");
            }
            return View(housingViewModel);
        }

        public IActionResult ShowSearchFrom()
        {
            return View(_ihousingManager.GetAllHousings());
        }

        public async Task<IActionResult> ShowSearchResults(String SearchPhrase, DateTime? from= null, DateTime? to = null)
        {
            return View("ShowSearchFrom", await _ihousingManager.SearchHousingAsync(SearchPhrase,from, to));
        }
    }
}