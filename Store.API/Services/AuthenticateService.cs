using Microsoft.AspNetCore.Identity;
using Store.API.Entities;
using Store.API.Models;

namespace Store.API.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AuthenticateService(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> RegisterUser(UserForCreationDto newUser)
        {
            var checkUsername = await _userManager.FindByNameAsync(newUser.UserName);
            var checkEmail = await _userManager.FindByNameAsync(newUser.Email);
            if (checkUsername != null)
                return IdentityResult.Failed(new IdentityError
                {
                    Description = "Username Already Exists!"
                });

            if (checkEmail != null)
                return IdentityResult.Failed(new IdentityError
                {
                    Description = "Email Already Exists!"
                });

            var result = await _userManager.CreateAsync(new AppUser()
            {
                UserName = newUser.UserName,
                Email = newUser.Email,
                PhoneNumber = newUser.PhoneNumber
            }, newUser.Password);

            return result;
        }
        

        public async Task<bool> LogIn(UserForLogIn input)
        {
            var user = await _userManager.FindByNameAsync(input.UserName);
            var checkPass = await _userManager.CheckPasswordAsync(user, input.Password);
            if (checkPass)
            {
                await _signInManager.PasswordSignInAsync(user, input.Password, false, false);
            }
            else
                return false;
            
            
            return checkPass;
        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
