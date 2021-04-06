using FindHousingProject.BLL.Interfaces;
using FindHousingProject.BLL.Models;
using FindHousingProject.Common.Constants;
using FindHousingProject.DAL.Entities;
using FindHousingProject.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FindHousingProject.Web.Controllers
{
    public class HousingController : Controller
    {
        private readonly IHousingManager _housingManager;
        private readonly IUserManager _usManager;

        public HousingController(
            IHousingManager housingManager,
            IUserManager usManager)
        {
            _housingManager = housingManager ?? throw new ArgumentNullException(nameof(housingManager));
            _usManager = usManager ?? throw new ArgumentNullException(nameof(usManager));
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.Name == null)
            {
                return RedirectToAction("SignIn", "Account");
            }
            else
            {
                var user = await _usManager.GetAsync(User.Identity.Name);
                if (user?.Role != RoleConstant.Owner)
                {
                    return RedirectToAction("Another", "Home");
                }
                else
                {
                    return View(await _housingManager.GetCurrentHousingsAsync(User.Identity.Name));
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
                var userId = await _usManager.GetUserIdByEmailAsync(User.Identity.Name);
                var placeDto = new Place()
                {
                    Name = housingViewModel.Place,
                };

                var housingDto = new HousingDto()
                {
                    UserId = userId,
                    Name = housingViewModel.Name,
                    Place = placeDto,
                    PricePerDay = housingViewModel.Price,
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
                    housingDto.Scenery = imageData;
                }

                await _housingManager.CreateAsync(housingDto);
                return RedirectToAction("Index", "Housing");
            }

            return View(housingViewModel);
        }

        public async Task<IActionResult> Delete(string housingId)
        {
            var housing = await _housingManager.GetHousingAsync(housingId);

            var housingEditViewModel = new HousingViewModel()
            {
                Id = housing.Id,
                Place = housing.Place?.Name,
                Description = housing.Description,
                Price = housing.PricePerDay,
                Name = housing.Name
            };

            return View(housingEditViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteHousing(string housingId)
        {
            var userId = await _usManager.GetUserIdByEmailAsync(User.Identity.Name);

            await _housingManager.DeleteAsync(housingId, userId);

            return RedirectToAction("Index", "Housing");
        }

        public async Task<IActionResult> Edit(string housingId)
        {
            var housing = await _housingManager.GetHousingAsync(housingId);

            var housingEditViewModel = new HousingViewModel()
            {
                Id = housing.Id,
                Place = housing.Place?.Name,
                Description = housing.Description,
                Price = housing.PricePerDay,
                Name = housing.Name,
                Address = housing.Address,
                Scenery = housing.Scenery
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
                    PricePerDay = housingViewModel.Price,
                    Address = housingViewModel.Address,
                    Name = housingViewModel.Name,
                    Scenery = housingViewModel.Scenery
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

                await _housingManager.UpdateHousingAsync(housingDto, userId);
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
                var housing = await _housingManager.GetHousingAsync(housingId);
                var user = await _usManager.GetAsync(User.Identity.Name);

                if (message != null)
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
                    Price = housing.PricePerDay,
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
                    PricePerDay = housingViewModel.Price,
                    Address = housingViewModel.Address,
                    Name = housingViewModel.Name,
                };

                return RedirectToAction("Index", "Housing");
            }

            return View(housingViewModel);
        }

        public IActionResult ShowSearchFrom()
        {
            return View(_housingManager.GetAllHousings());
        }

        public async Task<IActionResult> ShowSearchResults(String SearchPhrase, DateTime? from = null, DateTime? to = null)
        {
            return View("ShowSearchFrom", await _housingManager.SearchHousingAsync(SearchPhrase, from, to));
        }
    }
}