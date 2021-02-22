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
            return View(await _ihousingManager.GetCurrentHousingsAsync(User.Identity.Name));
            //return View(await _context.Joke.ToListAsync());
        }
        [Authorize]
        // GET: Jokes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Jokes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /* [Authorize]
         [HttpPost]
         [ValidateAntiForgeryToken]
          public async Task<IActionResult> Create([Bind("Id,JokeQuestion,JokeAnswer")] Joke joke)
          {
              if (ModelState.IsValid)
              {
                   _context.Add(joke);
                   await _context.SaveChangesAsync();
                   return RedirectToAction(nameof(Index));
               }
               return View(joke);
     }*/

        [HttpGet]
        [Authorize(Roles = RolesConstants.OwnerRole)]
        public IActionResult CreateHousing(string housingId)
        {
            var сreateHousingViewModel = new CreateHousingViewModel()
            {
                Id = housingId
            };
            return View(сreateHousingViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RolesConstants.OwnerRole)]
        public async Task<IActionResult> CreateHousing(CreateHousingViewModel сreateHousingViewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = await _usManager.GetUserIdByEmailAsync(User.Identity.Name);
                var housingDto = new Housing()
                {
                    Name = сreateHousingViewModel.Name,
                    Id= сreateHousingViewModel.Id,
                    Address = сreateHousingViewModel.Address
                };
                await _ihousingManager.CreateAsync(housingDto);
                return RedirectToAction("Housing", "Create");
            }
            return View(сreateHousingViewModel);
        }

    }
}