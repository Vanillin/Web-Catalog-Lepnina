using Api.Exceptions;
using Application.Request;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private IServiceUser _serviceUser;
    public UserController(IServiceUser serviceUser)
    {
        _serviceUser = serviceUser;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        return Ok(await _serviceUser.ReadById(id));
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _serviceUser.ReadAll());
    }

    [HttpGet("userInfo")]
    public async Task<IActionResult> GetUserInfo()
    {
        var userId = User.GetUserId();
        if (!userId.HasValue)
            return NotFound();

        return Ok(await _serviceUser.ReadById(userId.Value));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateUserRequest user)
    {
        return Ok(await _serviceUser.Update(user));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        return Ok(await _serviceUser.Delete(id));
    }
}
