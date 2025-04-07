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
        return Ok(await _serviceAttachment.ReadById(id));
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _serviceAttachment.ReadAll());
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateAttachmentRequest attachment)
    {
        return Ok(await _serviceAttachment.Create(attachment));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateAttachmentRequest attachment)
    {
        return Ok(await _serviceAttachment.Update(attachment));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        return Ok(await _serviceAttachment.Delete(id));
    }
}
