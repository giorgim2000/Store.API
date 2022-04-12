using Microsoft.AspNetCore.Identity;
using Store.API.Models;

namespace Store.API.Services
{
    public interface IAuthenticateService
    {
        Task<IdentityResult> RegisterUser(UserForCreationDto newUser);
        Task<SignInResult> LogIn(string logIn, string password);
        Task LogOut();
    }
}
