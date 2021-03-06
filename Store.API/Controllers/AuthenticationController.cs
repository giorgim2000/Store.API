using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.API.Models;
using Store.API.Services;

namespace Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticateService _authenticationService;
        public AuthenticationController(IAuthenticateService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost(nameof(RegisterUser))]
        public async Task<IActionResult> RegisterUser(UserForCreationDto newUser)
        {
            var result = await _authenticationService.RegisterUser(newUser);
            if (result.Succeeded)
                return NoContent();
            else
                return BadRequest(result.Errors.FirstOrDefault()?.Description);
        }
        [HttpPost(nameof(Authenticate))]
        public async Task<IActionResult> Authenticate(UserForLogIn input)
        {
            var result = await _authenticationService.LogIn(input);
            if (result)
                return Ok();
            else
                return Unauthorized();
        }

        [HttpPost(nameof(LogOut))]
        public async Task<IActionResult> LogOut()
        {
            await _authenticationService.LogOut();
            return Ok();
        }
    }
}
