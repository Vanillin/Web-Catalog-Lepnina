using Application.Request;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class AuthController(IServiceAuth _authService) : ControllerBase
    {
        [EnableRateLimiting("login")]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationUserRequest request)
        {
            var principal = await _authService.Register(request);
            await HttpContext.SignInAsync(principal);
            return Created();
        }

        [EnableRateLimiting("login")]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var principal = await _authService.Login(request);
            await HttpContext.SignInAsync(principal);
            return Created();
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }
    }
}
