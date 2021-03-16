using FindHousingProject.BLL.Interfaces;
using FindHousingProject.BLL.Models;
using FindHousingProject.Common.Constants;
using FindHousingProject.DAL.Entities;
using FindHousingProject.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FindHousingProject.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserManager _userManager;
        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [Route("user/{emailName?}")]
        [HttpGet]
        public async Task<IActionResult> Index(string email)
        {
            email = email ?? User.Identity.Name;
            var profile = await _userManager.GetAsync(email);
            var userViewModel = new UserViewModel()
            {
                Id = profile.Id,
                Email = profile.Email,
                FullName= profile.FullName,
                Avatar = profile.Avatar,
                Role = profile.Role,
                IsOwner = profile.IsOwner
            };

            return View(userViewModel);
        }
    }
}