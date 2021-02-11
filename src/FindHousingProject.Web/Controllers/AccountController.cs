using FindHousingProject.BLL.Interfaces;
using FindHousingProject.DAL.Entities;
using FindHousingProject.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindHousingProject.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;//private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;//private readonly SignInManager<User> _signInManager;
        private readonly IUserManager _iuserManager;
        public AccountController(IUserManager iuserManager,UserManager<User> userManager, SignInManager<User> signInManager)//public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _iuserManager = iuserManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { Email = model.Email, UserName = model.Email};//User user = new User { Email = model.Email, UserName = model.Email, Year = model.Year };
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "User");

                    //return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult SignIn(string returnUrl = null)
        {
            var signInViewModel = new SignInViewModel()
            {
                ReturnUrl = returnUrl
            };
            return View(signInViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    return RedirectToAction("Index", "User");
                }

                ModelState.AddModelError(string.Empty, "Invalid email and(or) password.");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = await _iuserManager.GetUserIdByEmailAsync(User.Identity.Name);
                var result = await _iuserManager.ChangePasswordAsync(userId, model.OldPassword, model.NewPassword);

                if (result.Succeeded)
                {
                    return RedirectToAction("Settings", "Account");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
        public async Task<IActionResult> Settings()
        {
            var userId = await _iuserManager.GetUserIdByEmailAsync(User.Identity.Name);
            return View(new SettingsViewModel());
        }
    }
}
