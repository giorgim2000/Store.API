using Microsoft.AspNetCore.Identity;
using Store.API.Models;

namespace Store.API.Services
{
    public interface IAuthenticateService
    {
        Task<IdentityResult> RegisterUser(UserForCreationDto newUser);
        Task<bool> LogIn(UserForLogIn input);
        Task LogOut();
    }
}
