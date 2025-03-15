using Application.Dto;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SectionController : ControllerBase
{
    private IServiceSection _serviceSection;
    public SectionController(IServiceSection serviceSection)
    {
        _serviceSection = serviceSection;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromQuery] int id)
    {
        var quiz = await _serviceSection.ReadById(id);
        return Ok(quiz);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var quiz = await _serviceSection.ReadAll();
        return Ok(quiz);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] SectionDto quiz)
    {
        await _serviceSection.Create(quiz);
        return Created();
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] SectionDto quiz)
    {
        var result = await _serviceSection.Update(quiz);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        var result = await _serviceSection.Delete(id);
        return Ok(result);
    }
}
