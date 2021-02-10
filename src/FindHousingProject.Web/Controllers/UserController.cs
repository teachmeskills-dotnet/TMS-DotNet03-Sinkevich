using FindHousingProject.BLL.Interfaces;
using FindHousingProject.DAL.Entities;
using FindHousingProject.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindHousingProject.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserManager _iuserManager;
        public UserController(IUserManager iuserManager)
        {
            _iuserManager = iuserManager;
        }

        [Route("profile/{emailName?}")]
        [HttpGet]
        public async Task<IActionResult> Index(string email)
        {
            email = email ?? User.Identity.Name;
            var profile = await _iuserManager.GetProfileAsync(email);

            var userViewModel = new UserViewModel()
            {
                Id = profile.Id,
                Email = profile.Email,
                FullName= profile.FullName,
                Avatar = profile.Avatar,
                IsOwner = profile.IsOwner
            };

            return View(userViewModel);
        }
    }
}