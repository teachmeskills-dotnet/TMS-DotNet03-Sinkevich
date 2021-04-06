using FindHousingProject.BLL.Interfaces;
using FindHousingProject.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FindHousingProject.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserManager _userManager;

        public UserController(IUserManager userManager)
        {
            _userManager = userManager ?? throw new System.ArgumentNullException(nameof(userManager));
        }

        [Route("user/{emailName?}")]
        [HttpGet]
        public async Task<IActionResult> Index(string email)
        {
            email ??= User.Identity.Name;

            var profile = await _userManager.GetAsync(email);
            var userViewModel = new UserViewModel()
            {
                Id = profile.Id,
                Email = profile.Email,
                FullName = profile.FullName,
                Avatar = profile.Avatar,
                Role = profile.Role,
                IsOwner = profile.IsOwner
            };

            return View(userViewModel);
        }
    }
}