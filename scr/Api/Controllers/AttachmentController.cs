using Application.Dto;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AttachmentController : ControllerBase
{
    private IServiceAttachment _serviceAttachment;
    public AttachmentController(IServiceAttachment serviceAttachment)
    {
        _serviceAttachment = serviceAttachment;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromQuery] int id)
    {
        var quiz = await _serviceAttachment.ReadById(id);
        return Ok(quiz);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var quiz = await _serviceAttachment.ReadAll();
        return Ok(quiz);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AttachmentDto quiz)
    {
        await _serviceAttachment.Create(quiz);
        return Created();
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] AttachmentDto quiz)
    {
        var result = await _serviceAttachment.Update(quiz);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        var result = await _serviceAttachment.Delete(id);
        return Ok(result);
    }
}
