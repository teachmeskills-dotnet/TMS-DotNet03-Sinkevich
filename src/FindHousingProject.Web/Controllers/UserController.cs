using FindHousingProject.BLL.Interfaces;
using FindHousingProject.Common.Constants;
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

        [Route("user/{emailName?}")]
        [HttpGet]
        public async Task<IActionResult> Index(string email)
        {
            email = email ?? User.Identity.Name;
            var profile = await _iuserManager.GetAsync(email);

            var userViewModel = new UserViewModel()
            {
                Id = profile.Id,
                Email = profile.Email,
                FullName= profile.FullName,
                Avatar = profile.Avatar,
                Role = profile.Role,
                Roles=new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> {new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem {
                    Value = RolesConstants.OwnerRole, Text= "Owner"},new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem{ Value = RolesConstants.GuestRole, Text= "Guest"} },
                IsOwner = profile.IsOwner
            };

            return View(userViewModel);
        }
    }
}