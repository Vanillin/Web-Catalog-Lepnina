using Application.Dto;
using Application.Request;
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
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var result = await _serviceAttachment.ReadById(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _serviceAttachment.ReadAll();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateAttachmentRequest attachment)
    {
        if (attachment == null) return NotFound();
        return Ok(await _serviceAttachment.Create(attachment));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateAttachmentRequest attachment)
    {
        if (attachment == null) return NotFound();
        return Ok(await _serviceAttachment.Update(attachment));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        var result = await _serviceAttachment.Delete(id);
        return Ok(result);
    }
}
