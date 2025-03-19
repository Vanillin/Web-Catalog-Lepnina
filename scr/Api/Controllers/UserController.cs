using Application.Dto;
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
    public async Task<IActionResult> Add([FromBody] UserDto user)
    {
        var result = await _serviceUser.Create(user);
        if (result == null) return BadRequest();
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UserDto user)
    {
        var result = await _serviceUser.Update(user);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        var result = await _serviceUser.Delete(id);
        return Ok(result);
    }
}
