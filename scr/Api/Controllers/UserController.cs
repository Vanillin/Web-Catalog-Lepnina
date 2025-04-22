using Application.Dto;
using Application.Request;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

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

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateUserRequest user)
    {
        return Ok(await _serviceUser.Create(user));
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
