using Application.Dto;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private IServiceProduct _serviceProduct;
    public ProductController(IServiceProduct serviceProduct)
    {
        _serviceProduct = serviceProduct;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromQuery] int id)
    {
        var quiz = await _serviceProduct.ReadById(id);
        return Ok(quiz);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var quiz = await _serviceProduct.ReadAll();
        return Ok(quiz);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] ProductDto quiz)
    {
        await _serviceProduct.Create(quiz);
        return Created();
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] ProductDto quiz)
    {
        var result = await _serviceProduct.Update(quiz);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        var result = await _serviceProduct.Delete(id);
        return Ok(result);
    }
}
