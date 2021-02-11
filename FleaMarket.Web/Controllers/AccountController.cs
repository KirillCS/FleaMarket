using FleaMarket.Models;
using FleaMarket.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;

namespace FleaMarket.Web.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IStringLocalizer<AccountController> localizer;

        public AccountController(UserManager<User> userManager,
                                 SignInManager<User> signInManager,
                                 IStringLocalizer<AccountController> localizer)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.localizer = localizer;
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult SignUp() => View();

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User(model.Name);
                var result = await userManager.CreateAsync(user, model.Password);
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
        [Route("[action]")]
        public IActionResult Login(string returnUrl = null) =>
            View(new LoginViewModel(returnUrl));

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Name, model.Password, model.IsPersistent, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl) && model.ReturnUrl != Url.Action())
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    return Redirect("/");
                }

                ModelState.AddModelError(string.Empty, localizer["IncorrectLoginOrPassword"]);
            }

            return View(model);
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await signInManager.SignOutAsync();

            return string.IsNullOrEmpty(returnUrl) ? Redirect("/") : Redirect(returnUrl);
        }
    }
}
