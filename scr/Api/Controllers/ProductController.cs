using Application.Dto;
using Application.Request;
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
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var result = await _serviceProduct.ReadById(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _serviceProduct.ReadAll();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateProductRequest product)
    {
        if (product == null) return NotFound();
        return Ok(await _serviceProduct.Create(product));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateProductRequest product)
    {
        if (product == null) return NotFound();
        return Ok(await _serviceProduct.Update(product));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        var result = await _serviceProduct.Delete(id);
        return Ok(result);
    }
}
