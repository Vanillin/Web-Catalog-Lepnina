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
        var result = await _serviceUser.ReadById(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _serviceUser.ReadAll();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateUserRequest user)
    {
        if (user == null) return NotFound();
        return Ok(await _serviceUser.Create(user));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateUserRequest user)
    {
        if (user == null) return NotFound();
        return Ok(await _serviceUser.Update(user));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        var result = await _serviceUser.Delete(id);
        return Ok(result);
    }
}
