using Application.Dto;
using Application.Request;
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
        return Ok(await _serviceReview.ReadById(id));
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _serviceReview.ReadAll());
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateReviewRequest review)
    {
        return Ok(await _serviceReview.Create(review));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateReviewRequest review)
    {
        return Ok(await _serviceReview.Update(review));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        return Ok(await _serviceReview.Delete(id));
    }
}
