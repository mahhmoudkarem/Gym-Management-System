using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.ViewModels.AccountViewModel;
using DAL.Entities.ApplicationUsers;
using Microsoft.AspNetCore.Identity;

namespace BLL.Services.AccountServices
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public AccountService(UserManager<ApplicationUser> _userManager)
        {
            userManager = _userManager;
        }
        public ApplicationUser? ValidateUser(LoginViewModel loginViewModel)
        {
           var User = userManager.FindByEmailAsync(loginViewModel.Email).Result;
           if (User is null) return null;

           var PasswordIsValid = userManager.CheckPasswordAsync(User, loginViewModel.Password).Result;
           return PasswordIsValid ? User : null;
        }
    }
}
