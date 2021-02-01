using FleaMarket.Models;
using FleaMarket.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleaMarket.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult SignUp() => View();

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User(model.Name);
                var result = await this.userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return Redirect("/");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null) =>
            View(new LoginViewModel(returnUrl));

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await this.signInManager.PasswordSignInAsync(model.Name, model.Password, model.IsPersistent, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    return Redirect("/");
                }

                ModelState.AddModelError(string.Empty, "Incorrect login or password");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();
            return Redirect("/");
        }
    }
}
