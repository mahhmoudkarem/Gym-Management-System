using BLL.Services.AccountServices;
using BLL.ViewModels.AccountViewModel;
using DAL.Entities.ApplicationUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Management_System.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(IAccountService accountService ,SignInManager<ApplicationUser> signInManager)
        {
            this.accountService = accountService;
            this.signInManager = signInManager;
        }

        #region Login

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if(!ModelState.IsValid) return View(model);
            var User = accountService.ValidateUser(model);
            if (User == null) {

                ModelState.AddModelError("InvalidLogin", "Invalid Email Or Password");
                return View(model);

            }
            var Result = signInManager.PasswordSignInAsync(User, model.Password, model.RememberMe, false).Result;
            if(Result.IsNotAllowed) 
                ModelState.AddModelError("InvalidLogin", "Your Account Is Not Allowed");
            if(Result.IsLockedOut) 
                ModelState.AddModelError("InvalidLogin", "Your Account Is Locked Out");
            if (Result.Succeeded)
                return RedirectToAction("Index", "Home");
            return View(model);
        }

        #endregion
        #region Logout
        [HttpPost]
        public IActionResult Logout()
        {
            signInManager.SignOutAsync().GetAwaiter().GetResult();
            return RedirectToAction(nameof(Login));
        }

        #endregion
        #region AccessDenied
         public IActionResult AccessDenied()
        {
            return View();
        }
        #endregion
    }
}
