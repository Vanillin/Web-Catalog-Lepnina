using Application.Dto;
using Application.Request;
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
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var result = await _serviceSection.ReadById(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _serviceSection.ReadAll();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateSectionRequest section)
    {
        if (section == null) return NotFound();
        return Ok(await _serviceSection.Create(section));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateSectionRequest section)
    {
        if (section == null) return NotFound();
        return Ok(await _serviceSection.Update(section));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        var result = await _serviceSection.Delete(id);
        return Ok(result);
    }
}
