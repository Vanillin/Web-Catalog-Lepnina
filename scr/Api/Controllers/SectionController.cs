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
        return Ok(await _serviceSection.ReadById(id));
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _serviceSection.ReadAll());
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateSectionRequest section)
    {
        return Ok(await _serviceSection.Create(section));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateSectionRequest section)
    {
        return Ok(await _serviceSection.Update(section));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        return Ok(await _serviceSection.Delete(id));
    }
}
