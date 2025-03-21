using Application.Dto;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ReviewController : ControllerBase
{
    private IServiceReview _serviceReview;
    public ReviewController(IServiceReview serviceReview)
    {
        _serviceReview = serviceReview;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var result = await _serviceReview.ReadById(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _serviceReview.ReadAll();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] ReviewDto review)
    {
        var result = await _serviceReview.Create(review);
        if (result == null) return BadRequest();
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ReviewDto review)
    {
        var result = await _serviceReview.Update(review);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        var result = await _serviceReview.Delete(id);
        return Ok(result);
    }
}
