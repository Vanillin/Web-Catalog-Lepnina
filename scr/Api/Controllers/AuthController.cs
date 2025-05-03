using Application.Request;
using Application.Services.Interfaces;
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
            await _authService.Register(request);
            return Created();
        }

        [EnableRateLimiting("login")]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _authService.Login(request);
            return Ok(response);
        }

    }
}
